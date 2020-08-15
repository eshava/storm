using System;
using System.Linq.Expressions;
using Eshava.Storm.MetaData.Enums;
using Eshava.Storm.MetaData.Extensions;
using Eshava.Storm.MetaData.Models;

namespace Eshava.Storm.MetaData.Builders
{
	public sealed class ReferenceOwnershipBuilder<TEntity, TRelatedEntity> where TEntity : class where TRelatedEntity : class
	{
		private readonly OwnsOneEntity _ownsOneEntity;

		internal ReferenceOwnershipBuilder(Property property)
		{
			_ownsOneEntity = property.OwnsOne;

			if (_ownsOneEntity == default)
			{
				_ownsOneEntity = new OwnsOneEntity(typeof(TRelatedEntity), ConfigurationSource.Explicit);
				property.SetOwnsOneEntity(_ownsOneEntity);
			}

		}

		public ReferenceOwnershipBuilder<TEntity, TRelatedEntity> OwnsOne<TNewRelatedEntity>(Expression<Func<TRelatedEntity, TNewRelatedEntity>> navigationExpression, Action<ReferenceOwnershipBuilder<TRelatedEntity, TNewRelatedEntity>> buildAction) where TNewRelatedEntity : class
		{
			var ownsOnePropertyName = navigationExpression.GetMemberAccess().GetSimpleMemberName();
			// Create property if not exists
			Property(navigationExpression);
			var ownsOneProperty = _ownsOneEntity.GetProperty(ownsOnePropertyName);

			var builder = new ReferenceOwnershipBuilder<TRelatedEntity, TNewRelatedEntity>(ownsOneProperty);
			buildAction(builder);

			return this;
		}

		public PropertyBuilder<TProperty> Property<TProperty>(Expression<Func<TRelatedEntity, TProperty>> propertyExpression)
		{
			return new PropertyBuilder<TProperty>(_ownsOneEntity, propertyExpression.GetMemberAccess(), ConfigurationSource.Explicit);
		}
	}
}