using System;

namespace Eshava.Storm.Extensions
{
	internal static class ObjectExtensions
	{
		public static object SanitizeParameterValue(this object value)
		{
			if (value == null)
			{
				return DBNull.Value;
			}

			if (value is Enum)
			{
				TypeCode typeCode;
				if (value is IConvertible)
				{
					typeCode = ((IConvertible)value).GetTypeCode();
				}
				else
				{
					typeCode = TypeExtensions.GetTypeCode(Enum.GetUnderlyingType(value.GetType()));
				}

				switch (typeCode)
				{
					case TypeCode.Byte:
						return (byte)value;
					case TypeCode.SByte:
						return (sbyte)value;
					case TypeCode.Int16:
						return (short)value;
					case TypeCode.Int32:
						return (int)value;
					case TypeCode.Int64:
						return (long)value;
					case TypeCode.UInt16:
						return (ushort)value;
					case TypeCode.UInt32:
						return (uint)value;
					case TypeCode.UInt64:
						return (ulong)value;
				}
			}

			return value;
		}
	}
}