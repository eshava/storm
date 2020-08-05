using System;
using System.Globalization;
using Eshava.Storm.Extensions;

namespace Eshava.Storm
{
	internal class DataTypeMapper
	{
		public T Map<T>(object value)
		{
			var mappedValue = Map(typeof(T), value);

			return (T)mappedValue;
		}

		public object Map(Type type, object value)
		{
			if (value == null || value == DBNull.Value)
			{
				if (type.IsValueType && !type.IsDataTypeNullable())
				{
					return Activator.CreateInstance(type);
				}

				return null;
			}

			var mappedValue = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);

			return mappedValue;
		}
	}
}