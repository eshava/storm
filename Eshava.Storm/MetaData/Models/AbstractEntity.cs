using System;
using System.Collections.Generic;
using System.Linq;
using Eshava.Storm.MetaData.Enums;

namespace Eshava.Storm.MetaData.Models
{
	internal abstract class AbstractEntity
	{
		internal AbstractEntity(Type type, ConfigurationSource configurationSource)
		{
			Type = type;
			ConfigurationSource = configurationSource;
			Properties = new Dictionary<string, Property>();
		}

		public Type Type { get; }
		public ConfigurationSource ConfigurationSource { get; set; }
		protected Dictionary<string, Property> Properties { get; }

		public Property GetProperty(string propertyName)
		{
			if (!Properties.ContainsKey(propertyName))
			{
				return default;
			}

			return Properties[propertyName];
		}

		public void AddProperty(Property property)
		{
			Properties.Add(property.Name, property);
		}

		public IEnumerable<Property> GetProperties()
		{
			return Properties.Values.Where(p => !p.IsIgnored);
		}
	}
}