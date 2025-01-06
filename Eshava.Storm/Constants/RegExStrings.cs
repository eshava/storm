using System.Text.RegularExpressions;

namespace Eshava.Storm.Constants
{
	internal static class RegExStrings
	{
		public static readonly Regex LiteralTokens = new Regex(@"(?<![a-z0-9_])\{=([a-z0-9_]+)\}", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

		public static readonly Regex TablesAliases = new Regex(@"(\bFROM\b|\bJOIN\b)\s+(?<table>\[?\b\S+\b\]?)(AS|FOR|SYSTEM_TIME|OF|@{1}\S*|\s)*\s+(?<tablealias>\[?(?!\b(ON|JOIN|INNER|OUTER|LEFT|RIGHT|CROSS|WHERE)\b)(\s{0}\S+)\]?)\s*", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
		
		public static readonly Regex SelectJoinAliases = new Regex(@"(\))\s+\[?(\b\S+\b)\]?\s+(\bON\b)\s+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
		public static readonly Regex SelectJoinAliasesWithAs = new Regex(@"(\))\s+(\bAS\b)\s+\[?(\b\S+\b)\]?\s+(\bON\b)\s+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

		public static readonly Regex SelectJoinAliasesInclusiveInnerSelect = new Regex(@"(\bJOIN\b\s*)(\(){1}([\s\S]+?)(\))\s+\[?(\b\S+\b)\]?\s+(\bON\b)\s+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
		public static readonly Regex SelectJoinAliasesWithAsInclusiveInnerSelect = new Regex(@"(\bJOIN\b\s*)(\(){1}([\s\S]+?)(\))\s+(\bAS\b)\s+\[?(\b\S+\b)\]?\s+(\bON\b)\s+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
	}
}