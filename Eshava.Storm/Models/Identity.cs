using System;
using System.Data;

namespace Eshava.Storm.Models
{
	/// <summary>
	/// Identity of a cached query in Dapper, used for extensibility
	/// </summary>
	public class Identity : IEquatable<Identity>
	{
		internal Identity(
			string sql,
			CommandType? commandType,
			IDbConnection connection,
			Type type,
			Type parametersType)
		{
			Initiate(sql, commandType, connection.ConnectionString, type, parametersType);
		}

		private void Initiate(string sql, CommandType? commandType, string connectionString, Type type, Type parametersType)
		{
			Sql = sql;
			CommandType = commandType;
			ConnectionString = connectionString;
			Type = type;
			ParametersType = parametersType;

			unchecked
			{
				HashCode = 17; // we *know* we are using this in a dictionary, so pre-compute this
				HashCode = HashCode * 23 + commandType.GetHashCode();
				HashCode = HashCode * 23 + (sql?.GetHashCode() ?? 0);
				HashCode = HashCode * 23 + (type?.GetHashCode() ?? 0);
				HashCode = HashCode * 23 + (connectionString == null ? 0 : StringComparer.Ordinal.GetHashCode(connectionString));
				HashCode = HashCode * 23 + (parametersType?.GetHashCode() ?? 0);
			}
		}

		public string Sql { get; private set; }

		public CommandType? CommandType { get; private set; }

		public int HashCode { get; private set; }

		public Type Type { get; private set; }

		public string ConnectionString { get; private set; }

		public Type ParametersType { get; private set; }

		public override int GetHashCode()
		{
			return HashCode;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Identity);
		}

		public bool Equals(Identity other)
		{
			return other != null
				&& Type == other.Type
				&& Sql == other.Sql
				&& CommandType == other.CommandType
				&& StringComparer.Ordinal.Equals(ConnectionString, other.ConnectionString)
				&& ParametersType == other.ParametersType;
		}
	}
}