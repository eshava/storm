using System.Reflection;

namespace Eshava.Storm.Models
{
	public class ReaderAccessItem
	{
		public int Ordinal { get; set; }
		public object Instance { get; set; }
		public PropertyInfo PropertyInfo { get; set; }
	}
}