using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Eshava.Storm.Linq.Enums;
using Eshava.Storm.Linq.Extensions;
using Eshava.Storm.Linq.Models;

namespace Eshava.Storm.Linq.Engines
{
	internal class WhereQueryEngine : AbstractQueryEngine
	{
		private const string SQL_WHERE = "WHERE";

		public WhereQueryResult AddWhereConditionsToQuery<T>(IEnumerable<Expression<Func<T, bool>>> queryConditions, string sqlQuery, WhereQuerySettings settings)
		{
			var whereQueryResult = CalculateWhereConditions(queryConditions, settings);

			if (whereQueryResult.Sql.IsNullOrEmpty())
			{
				whereQueryResult.Sql = sqlQuery;

				return whereQueryResult;
			}

			var existence = sqlQuery.CheckExistence(SQL_WHERE);
			if (existence == Existence.Available)
			{
				whereQueryResult.Sql = String.Join(Environment.NewLine, sqlQuery, SQL_AND, whereQueryResult.Sql);
			}
			else
			{
				whereQueryResult.Sql = String.Join(Environment.NewLine, sqlQuery, SQL_WHERE, whereQueryResult.Sql);
			}

			return whereQueryResult;
		}

		public WhereQueryResult CalculateWhereConditions<T>(IEnumerable<Expression<Func<T, bool>>> queryConditions, WhereQuerySettings settings)
		{
			var result = new WhereQueryResult(settings?.QueryParameter);

			if (!(queryConditions?.Any() ?? false))
			{
				return result;
			}

			var data = new WhereQueryData
			{
				PropertyMappings = settings?.PropertyMappings ?? new Dictionary<string, string>(),
				QueryParameter = result.QueryParameter
			};

			var sql = new StringBuilder();
			foreach (var queryCondition in queryConditions)
			{
				if (sql.Length > 0)
				{
					sql.AppendLine("AND");
				}

				sql.AppendLine(ProcessExpression(queryCondition.Body, data));
			}

			result.Sql = sql.ToString();

			return result;
		}
	}
}