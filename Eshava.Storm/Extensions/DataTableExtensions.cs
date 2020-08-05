using System.Data;
using Eshava.Storm.Constants;

namespace Eshava.Storm.Extensions
{
	public static class DataTableExtensions
	{
		public static void SetTypeName(this DataTable table, string typeName)
		{
			if (table != null)
			{
				if (typeName.IsNullOrEmpty())
				{
					table.ExtendedProperties.Remove(DefaultNames.DATATABLETYPENAMEKEY);
				}
				else
				{
					table.ExtendedProperties[DefaultNames.DATATABLETYPENAMEKEY] = typeName;
				}
			}
		}

		/// <summary>
		/// Fetch the type name associated with a DataTable
		/// </summary>
		public static string GetTypeName(this DataTable table)
		{
			return table?.ExtendedProperties[DefaultNames.DATATABLETYPENAMEKEY] as string;
		}
	}
}