using System;
using System.Data;
using System.Reflection;
using Eshava.Storm.Extensions;
using Eshava.Storm.Interfaces;
using Microsoft.Data.SqlClient;

namespace Eshava.Storm.QueryParameters
{
	internal class TableParameter : ICustomQueryParameter
	{
		private readonly DataTable _table;
		private readonly string _typeName;

		internal TableParameter(DataTable table) : this(table, null) { }

		internal TableParameter(DataTable table, string typeName)
		{
			_table = table;
			_typeName = typeName;
		}

		static TableParameter()
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
			var param = command.CreateParameter();
			param.ParameterName = name;
			Set(param, _table, _typeName);
			command.Parameters.Add(param);
		}

		internal static void Set(IDbDataParameter parameter, DataTable table, string typeName)
		{
			parameter.Value = table.SanitizeParameterValue();

			if (typeName.IsNullOrEmpty() && table != null)
			{
				typeName = table.GetTypeName();
			}

			if (!typeName.IsNullOrEmpty() && parameter is SqlParameter sqlParam)
			{
				SetTypeName?.Invoke(sqlParam, typeName);
				sqlParam.SqlDbType = SqlDbType.Structured;
			}
		}
	}
}
