using System.Collections.Generic;
using System.Text;

namespace Eshava.Storm.Models
{
	internal class WhereCondition
	{
		public StringBuilder Query { get; set; }
		public string TableName { get; set; }
		public IEnumerable<KeyProperty> Properties { get; set; }
		public List<KeyValuePair<string, object>> Parameters { get; set; }
	}
}