﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Eshava.Storm.MetaData.Enums;
using Eshava.Storm.MetaData.Extensions;
using Eshava.Storm.MetaData.Models;

namespace Eshava.Storm.MetaData.Builders
{
	public class PropertyBuilder
	{
		private readonly Property _property;

		internal PropertyBuilder(AbstractEntity entity, MemberInfo memberInfo, ConfigurationSource configurationSource)
		{
			var propertyName = memberInfo.GetSimpleMemberName();
			_property = entity.GetProperty(propertyName);

			if (_property == default)
			{
				_property = new Property(
					 propertyName,
					 memberInfo.GetMemberType(),
					 memberInfo as PropertyInfo,
					 configurationSource
				);
				entity.AddProperty(_property);
			}
		}

		public void IsPrimaryKey()
		{
			_property.SetPrimiaryKey(true, ConfigurationSource.Explicit);
		}

		public void IsReadOnly()
		{
			_property.SetIsReadOnly();
		}

		public void Ignore()
		{
			_property.Ignore();
		}

		protected void ValueGenerated(DatabaseGeneratedOption option)
		{
			_property.SetAutoGeneratedOption(option);
		}
		
		protected void SetColumnName(string columnName)
		{
			_property.SetColumnName(columnName);
		}
	}
}