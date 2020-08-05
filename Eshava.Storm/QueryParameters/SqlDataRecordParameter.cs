using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Eshava.Storm.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;

namespace Eshava.Storm.QueryParameters
{
	internal class SqlDataRecordParameter : ICustomQueryParameter
	{
		private readonly IEnumerable<SqlDataRecord> _data;
		private readonly string _typeName;


		public SqlDataRecordParameter(IEnumerable<SqlDataRecord> data, string typeName)
		{
			_data = data;
			_typeName = typeName;
		}

		static SqlDataRecordParameter()
		{
			var prop = typeof(SqlParameter).GetProperty("TypeName", BindingFlags.Instance | BindingFlags.Public);
			if (prop != null && prop.PropertyType == typeof(string) && prop.CanWrite)
			{
				SetTypeName = (Action<SqlParameter, string>)Delegate.CreateDelegate(typeof(Action<SqlParameter, string>), prop.GetSetMethod());
			}
		}

		internal static Action<SqlParameter, string> SetTypeName { get; }

		public void AddParameter(IDbCommand command, string name)
		{
			var parameter = command.CreateParameter();
			parameter.ParameterName = name;
			Set(parameter, _data, _typeName);
			command.Parameters.Add(parameter);
		}

		internal static void Set(IDbDataParameter parameter, IEnumerable<SqlDataRecord> data, string typeName)
		{
			parameter.Value = (object)data ?? DBNull.Value;

			var sqlParam = parameter as SqlParameter;
			if (sqlParam != null)
			{
				sqlParam.SqlDbType = SqlDbType.Structured;
				sqlParam.TypeName = typeName;
			}
		}
	}
}