using System;
using System.Data;
using Eshava.Storm.Extensions;
using Eshava.Storm.Interfaces;
using Microsoft.Data.SqlClient;

namespace Eshava.Storm.Models
{
	internal class UdtTypeHandler : ITypeHandler
	{
		private readonly string _udtTypeName;

		public bool ReadAsByteArray => false;

		public UdtTypeHandler(string udtTypeName)
		{
			if (udtTypeName.IsNullOrEmpty())
			{
				throw new ArgumentException("Cannot be null or empty", udtTypeName);
			}

			_udtTypeName = udtTypeName;
		}

		public object Parse(Type destinationType, object value)
		{
			return value is DBNull ? null : value;
		}

		public void SetValue(IDbDataParameter parameter, object value)
		{
			parameter.Value = value.SanitizeParameterValue();
			if (parameter is SqlParameter && !(value is DBNull))
			{
				((SqlParameter)parameter).SqlDbType = SqlDbType.Udt;
				((SqlParameter)parameter).UdtTypeName = _udtTypeName;
			}
		}
	}
}