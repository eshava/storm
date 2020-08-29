﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Eshava.Storm
{
	/// <summary>
	/// Permits specifying certain SqlMapper values globally.
	/// </summary>
	public static class Settings
	{
		static Settings()
		{
			SetDefaults();
		}

		/// <summary>
		/// Resets all Settings to their default values
		/// </summary>
		public static void SetDefaults()
		{
			CommandTimeout = null;
			IgnoreDuplicatedColumns = false;
			DefaultKeyColumnValueGeneration = DatabaseGeneratedOption.None;
		}

		/// <summary>
		/// Specifies the default Command Timeout for all Queries
		/// </summary>
		public static int? CommandTimeout { get; set; }

		/// <summary>
		/// If a column name is duplicated, the duplicates are skipped (by default, all are processed, so the last column wins)
		/// Hint: Will be ignored if an table alias is passed for object mapping 
		/// </summary>
		/// <remarks>This setting should be set once at the start of the application; it is not intended to be toggled per-query</remarks>
		public static bool IgnoreDuplicatedColumns { get; set; }

		/// <summary>
		/// If the DatabaseGenerated attribute is not set, the option specifies whether the value of a key column is assumed to be autogenerated.
		/// </summary>
		public static DatabaseGeneratedOption DefaultKeyColumnValueGeneration { get; set; }

		/// <summary>
		/// Date and time data. Date value with an accuracy of 100 nanoseconds.
		/// </summary>
		public static bool EnableDateTimeHighAccuracy { get; set; }
	}
}