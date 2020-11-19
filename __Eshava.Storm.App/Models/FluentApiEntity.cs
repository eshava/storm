using System;
using System.Collections.Generic;
using System.Text;

namespace Eshava.Storm.App.Models
{
	public class FluentApiEntity
	{

		public int PrimiaryKey { get; set; }

		public string Name { get; set; }

		public FluentApiOwnsOneEntity OwnsOneProperty { get; set; }
	}
}
