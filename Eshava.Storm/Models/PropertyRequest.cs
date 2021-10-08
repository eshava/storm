using System;
using System.Collections.Generic;

namespace Eshava.Storm.Models
{
	internal class PropertyRequest
	{
		public Type Type { get; set; }
		public object Entity { get; set; }
		public string NamePrefix { get; set; }
		public object PartialEntity { get; set; }
		public IEnumerable<KeyValuePair<string, object>> PatchProperties { get; set; }
		public MetaData.Models.AbstractEntity EntityTypeResult { get; set; }
	}
}