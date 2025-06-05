using System;

namespace Eshava.Storm.Models
{
	internal class ColumnCacheItem
	{
		public ColumnCacheItem(int ordinal, Type dataType, string columnName, string tableName)
		{
			Ordinal = ordinal;
			DataType = dataType;
			ColumnName = columnName;
			TableName = tableName;
		}

		public string ColumnName { get; set; }
		public string TableName { get; set; }

		public int Ordinal { get; set; }
		public Type DataType { get; set; }
		public string TableAlias { get; set; }
	}
}