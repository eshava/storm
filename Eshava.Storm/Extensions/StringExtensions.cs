using System;
using System.Collections.Generic;
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
			var hasMatchesAliasesWithAS = RegExStrings.TablesAliasesWithAS.IsMatch(sql);

			if (!hasMatchesAliases && !hasMatchesAliasesWithAS)
			{
				return tableAliases;
			}

			if (hasMatchesAliases)
			{
				var matches = RegExStrings.TablesAliases.Matches(sql);
				ExecuteRegEx(matches, 3, tableAliases);
			}

			if (hasMatchesAliasesWithAS)
			{
				var matches = RegExStrings.TablesAliasesWithAS.Matches(sql);
				ExecuteRegEx(matches, 4, tableAliases);
			}

			return tableAliases;
		}

		private static void ExecuteRegEx(MatchCollection matches, int secondGroupIndex, Dictionary<string, string> tableAliases)
		{
			foreach (Match match in matches)
			{
				var tableName = match.Groups[2].Value.CleanTableName();
				var alias = match.Groups[secondGroupIndex].Value.CleanTableName();

				if (alias.IsNullOrEmpty())
				{
					continue;
				}

				tableAliases.Add(alias, tableName);
			}
		}

		internal static string CleanTableName(this string tableName)
		{
			return tableName
				.ToLower()
				.Replace("[", "")
				.Replace("]", "")
				.Trim()
				;
		}
	}
}