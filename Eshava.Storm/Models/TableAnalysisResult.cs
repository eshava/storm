using System.Collections.Generic;

namespace Eshava.Storm.Models
{
	internal class TableAnalysisResult
	{
		public Dictionary<string, IList<string>> TableAliases { get; set; }
		public Dictionary<string, int> AliasOccurrences { get; set; }

		public Dictionary<string, IList<ColumnCacheItem>> ColumnCache { get; set; }
		public IEnumerable<string> ResultTableNames { get; set; }
		public string SqlQuery { get; set; }
	}
}