using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Eshava.Storm.MetaData.Enums;
using Eshava.Storm.MetaData.Models;

namespace Eshava.Storm.MetaData.Builders
{
	public sealed class PropertyBuilder<TProperty> : PropertyBuilder
	{
		internal PropertyBuilder(AbstractEntity entity, MemberInfo memberInfo, ConfigurationSource configurationSource)
			: base(entity, memberInfo, configurationSource)
		{

		}

		public PropertyBuilder<TProperty> ValueGeneratedNever()
		{
			ValueGenerated(DatabaseGeneratedOption.None);

			return this;
		}

		public PropertyBuilder<TProperty> ValueGeneratedOnAdd()
		{
			ValueGenerated(DatabaseGeneratedOption.Identity);

			return this;
		}

		public PropertyBuilder<TProperty> ValueGeneratedOnAddOrUpdate()
		{
			ValueGenerated(DatabaseGeneratedOption.Computed);

			return this;
		}

		public PropertyBuilder<TProperty> HasColumnName(string columnName)
		{
			SetColumnName(columnName);

			return this;
		}

		public PropertyBuilder<TProperty> IsKey()
		{
			IsPrimaryKey();

			return this;
		}
	}
}