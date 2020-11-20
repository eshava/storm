using System.Collections.Generic;

namespace Eshava.Storm.Linq.Models
{
	public class WhereQueryResult
	{
		public WhereQueryResult(Dictionary<string, object> queryParameter)
		{
			Sql = "";
			QueryParameter = queryParameter ?? new Dictionary<string, object>();
		}

		public string Sql { get; set; }
		public Dictionary<string, object> QueryParameter { get; set; }
	}
}