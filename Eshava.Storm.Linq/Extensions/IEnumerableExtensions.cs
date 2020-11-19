using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Eshava.Core.Linq.Models;
using Eshava.Storm.Linq.Engines;
using Eshava.Storm.Linq.Models;

namespace Eshava.Storm.Linq.Extensions
{
	public static class IEnumerableExtensions
	{
		public static WhereQueryResult AddWhereConditionsToQuery<T>(this IEnumerable<Expression<Func<T, bool>>> queryConditions, string sqlQuery, WhereQuerySettings settings = null) where T : class
		{
			var whereQueryEngine = new WhereQueryEngine();

			return whereQueryEngine.AddWhereConditionsToQuery(queryConditions, sqlQuery, settings);
		}

		public static WhereQueryResult CalculateWhereConditions<T>(this IEnumerable<Expression<Func<T, bool>>> queryConditions, WhereQuerySettings settings) where T : class
		{
			var whereQueryEngine = new WhereQueryEngine();

			return whereQueryEngine.CalculateWhereConditions(queryConditions, settings);
		}

		public static string AddSortConditionsToQuery(this IEnumerable<OrderByCondition> orderByConditions, string sqlQuery, QuerySettings settings = null)
		{
			var sortingQueryEngine = new SortingQueryEngine();

			return sortingQueryEngine.AddSortConditionsToQuery(orderByConditions, sqlQuery, settings);
		}

		public static string CalculateSortConditions(this IEnumerable<OrderByCondition> orderByConditions, QuerySettings settings = null)
		{
			var sortingQueryEngine = new SortingQueryEngine();

			return sortingQueryEngine.CalculateSortConditions(orderByConditions, settings);
		}
	}
}