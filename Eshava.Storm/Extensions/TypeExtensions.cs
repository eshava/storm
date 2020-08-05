using System;
using System.Collections;
using System.Data;
using System.Linq;
using Eshava.Storm.Constants;
using Eshava.Storm.Interfaces;
using Eshava.Storm.Models;

namespace Eshava.Storm.Extensions
{
	internal static class TypeExtensions
	{

		internal static DbType LookupDbType(this Type type, string name, bool demand, out ITypeHandler handler)
		{
			DbType dbType;
			handler = null;
			var nullUnderlyingType = Nullable.GetUnderlyingType(type);
			if (nullUnderlyingType != null)
			{
				type = nullUnderlyingType;
			}

			if (type.IsEnum && !DbTypeMap.Map.ContainsKey(type))
			{
				type = Enum.GetUnderlyingType(type);
			}

			if (DbTypeMap.Map.TryGetValue(type, out dbType))
			{
				return dbType;
			}

			if (type.FullName == DefaultNames.LINQBINARY)
			{
				return DbType.Binary;
			}
			
			if (TypeHandlerMap.Map.TryGetValue(type, out handler))
			{
				return DbType.Object;
			}
			
			switch (type.FullName)
			{
				case "Microsoft.SqlServer.Types.SqlGeography":
					TypeHandlerMap.Add(type, handler = new UdtTypeHandler("geography"));
					return DbType.Object;
				case "Microsoft.SqlServer.Types.SqlGeometry":
					TypeHandlerMap.Add(type, handler = new UdtTypeHandler("geometry"));
					return DbType.Object;
				case "Microsoft.SqlServer.Types.SqlHierarchyId":
					TypeHandlerMap.Add(type, handler = new UdtTypeHandler("hierarchyid"));
					return DbType.Object;
			}

			if (demand)
			{
				throw new NotSupportedException($"The member {name} of type {type.FullName} cannot be used as a parameter value");
			}

			return DbType.Object;

		}

		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		internal static Type GetDataType(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			if (type.IsDataTypeNullable())
			{
				type = Nullable.GetUnderlyingType(type);
			}

			return type;
		}

		internal static bool IsDataTypeNullable(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		internal static bool IsClass(this Type type)
		{
			return type.IsClass
				&& !Equals(type, typeof(string))
				&& !type.ImplementsIEnumerable()
				;
		}

		internal static bool ImplementsIEnumerable(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return type.IsGenericType && ImplementsInterface(type, typeof(IEnumerable));
		}

		internal static bool ImplementsInterface(this Type type, Type interfaceType)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			if (interfaceType == null)
			{
				throw new ArgumentNullException(nameof(interfaceType));
			}

			return type.GetInterfaces().Any(t => t == interfaceType);
		}
	}
}