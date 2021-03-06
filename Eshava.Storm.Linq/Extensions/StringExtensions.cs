﻿using System;
using System.Linq;
using Eshava.Storm.Linq.Enums;

namespace Eshava.Storm.Linq.Extensions
{
	public static class StringExtensions
	{
		public static string AppendSkipAndTake(this string sqlQuery, int skip, int take)
		{
			if (take <= 0)
			{
				return sqlQuery;
			}

			return $"{sqlQuery}{Environment.NewLine}OFFSET {Math.Max(skip, 0)} ROWS FETCH NEXT {take} ROWS ONLY";
		}

		internal static bool IsNullOrEmpty(this string text)
		{
			return String.IsNullOrEmpty(text);
		}

		internal static Existence CheckExistence(this string query, string keyWord)
		{
			var index = query.ToUpper().LastIndexOf(keyWord);
			if (index < 0)
			{
				// no where key word

				return Existence.None;
			}

			var queryPart = query.Substring(index);
			var openCount = queryPart.Count(c => c == '(');
			var closeCount = queryPart.Count(c => c == ')');

			if (openCount < closeCount)
			{
				// where key word belongs to an inner sql statement

				return Existence.None;
			}

			// where key word belongs to the main sql statement

			return Existence.Available;
		}
	}
}