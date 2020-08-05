using System.Text.RegularExpressions;

namespace Eshava.Storm.Constants
{
	internal static class RegExStrings
	{
		public static readonly Regex LiteralTokens = new Regex(@"(?<![a-z0-9_])\{=([a-z0-9_]+)\}", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

		public static readonly Regex TablesAliases = new Regex(@"(\bFROM\b|\bJOIN\b)\s+\b(\S+)(\b\s+\b(?!ON)\S+\b)\s*", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

	}
}
