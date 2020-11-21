using System;
using System.Collections.Generic;

namespace Eshava.Storm.Linq.Models
{
	public class QuerySettings
	{
		public Dictionary<string, string> PropertyMappings { get; set; }
		public Dictionary<Type, string> PropertyTypeMappings { get; set; }
	}
}