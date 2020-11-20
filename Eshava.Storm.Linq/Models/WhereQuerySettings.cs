using System.Collections.Generic;

namespace Eshava.Storm.Linq.Models
{
	public class WhereQuerySettings : QuerySettings
	{
		public Dictionary<string, object> QueryParameter { get; set; }
	}
}