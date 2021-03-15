using System.Collections.Concurrent;

namespace Eshava.Storm.Models
{
	internal static class ReaderInformationCache
	{
		private readonly static ConcurrentDictionary<int, TableAnalysisResult> _queryCache = new ConcurrentDictionary<int, TableAnalysisResult>();

		public static TableAnalysisResult GetReaderTableAnalysisResult(int hashCode)
		{
			if (_queryCache.TryGetValue(hashCode, out var analysis))
			{
				return analysis;
			}

			return default;
		}

		public static void AddTableAnalysisResult(int hashCode, TableAnalysisResult analysis)
		{
			_queryCache.TryAdd(hashCode, analysis);
		}
	}
}