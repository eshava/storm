using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Eshava.Storm.Models
{
	internal static class ReaderInformationCache
	{
		private readonly static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _typeCache = new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();
		private readonly static ConcurrentDictionary<int, TableAnalysisResult> _queryCache = new ConcurrentDictionary<int, TableAnalysisResult>();

		public static IEnumerable<PropertyInfo> GetReaderProperties(Type type)
		{
			if (_typeCache.TryGetValue(type, out var propertyInfos))
			{
				return propertyInfos;
			}

			return Array.Empty<PropertyInfo>();
		}

		public static void AddType(Type type, IEnumerable<PropertyInfo> propertyInfos)
		{
			_typeCache.TryAdd(type, propertyInfos);
		}

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