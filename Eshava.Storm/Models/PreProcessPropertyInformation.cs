using System.Collections.Generic;
using Eshava.Storm.MetaData.Models;

namespace Eshava.Storm.Models
{
	internal class PreProcessPropertyInformation
	{
		public PreProcessPropertyInformation()
		{
			ColumnPrefix = "";
		}

		public IList<ReaderAccessItem> ReaderAccessItems { get; set; }
		public object Instance { get; set; }
		public IEnumerable<(string Alias, IList<string> TableNames)> RequestedTableNames { get; set; }
		public string ColumnPrefix { get; set; }
		public AbstractEntity Entity { get; set; }
	}
}