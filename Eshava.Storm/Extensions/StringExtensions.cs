using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Eshava.Storm.Constants;
using Eshava.Storm.Models;

namespace Eshava.Storm.Extensions
{
	internal static class StringExtensions
	{
		internal static bool IsNullOrEmpty(this string text)
		{
			return String.IsNullOrEmpty(text);
		}

		internal static string Clean(this string name)
		{
			if (!IsNullOrEmpty(name))
			{
				switch (name[0])
				{
					case '@':
					case ':':
					case '?':
						return name.Substring(1);
				}
			}

			return name;
		}

		internal static IEnumerable<LiteralToken> GetLiteralTokens(this string sql)
		{
			if (sql.IsNullOrEmpty())
			{
				return LiteralToken.None;
			}

			if (!RegExStrings.LiteralTokens.IsMatch(sql))
			{
				return LiteralToken.None;
			}

			var matches = RegExStrings.LiteralTokens.Matches(sql);
			var found = new HashSet<string>(StringComparer.Ordinal);
			var list = new List<LiteralToken>(matches.Count);

			foreach (Match match in matches)
			{
				var token = match.Value;
				if (found.Add(match.Value))
				{
					list.Add(new LiteralToken(token, match.Groups[1].Value));
				}
			}

			return list.Count == 0
				? LiteralToken.None
				: list;
		}

		internal static Dictionary<string, string> GetTableAliases(this string sql)
		{
			var tableAliases = new Dictionary<string, string>();

			if (sql.IsNullOrEmpty())
			{
				return tableAliases;
			}

			var hasMatchesAliases = RegExStrings.TablesAliases.IsMatch(sql);
			var hasMatchesAliasesWithAS = RegExStrings.TablesAliasesWithAs.IsMatch(sql);
			var hasMatchesSelectJoinsAliases = RegExStrings.SelectJoinAliases.IsMatch(sql);
			var hasMatchesSelectJoinsAliasesWithAS = RegExStrings.SelectJoinAliasesWithAs.IsMatch(sql);


			if (!hasMatchesAliases && !hasMatchesAliasesWithAS)
			{
				return tableAliases;
			}

			if (hasMatchesAliases)
			{
				var matches = RegExStrings.TablesAliases.Matches(sql);
				ExecuteTableRegEx(matches, 4, tableAliases);
			}

			if (hasMatchesAliasesWithAS)
			{
				var matches = RegExStrings.TablesAliasesWithAs.Matches(sql);
				ExecuteTableRegEx(matches, 4, tableAliases);
			}

			if (hasMatchesSelectJoinsAliases)
			{
				var matches = RegExStrings.SelectJoinAliases.Matches(sql);
				ExecuteSelectJoinRegEx(matches, 2, tableAliases);
			}

			if (hasMatchesSelectJoinsAliasesWithAS)
			{
				var matches = RegExStrings.SelectJoinAliasesWithAs.Matches(sql);
				ExecuteSelectJoinRegEx(matches, 3, tableAliases);
			}

			return tableAliases;
		}

		private static void ExecuteTableRegEx(MatchCollection matches, int secondGroupIndex, Dictionary<string, string> tableAliases)
		{
			foreach (Match match in matches)
			{
				var tableName = match.Groups[2].Value.CleanTableName();
				var alias = match.Groups[secondGroupIndex].Value.CleanTableName();

				if (alias.IsNullOrEmpty())
				{
					continue;
				}

				if (!tableAliases.ContainsKey(alias))
				{
					tableAliases.Add(alias, tableName);
				}
			}
		}

		private static void ExecuteSelectJoinRegEx(MatchCollection matches, int secondGroupIndex, Dictionary<string, string> tableAliases)
		{
			foreach (Match match in matches)
			{
				var alias = match.Groups[secondGroupIndex].Value.CleanTableName();

				if (alias.IsNullOrEmpty())
				{
					continue;
				}

				if (!tableAliases.ContainsKey(alias))
				{
					tableAliases.Add(alias, "*");
				}
			}
		}

		internal static string CleanTableName(this string tableName)
		{
			return tableName
				.ToLower()
				.Replace("[", "")
				.Replace("]", "")
				.Trim()
				.Split('.')
				.Last()
				;
		}
	}
}