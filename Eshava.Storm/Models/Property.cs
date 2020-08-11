using System.Reflection;
using Eshava.Storm.Interfaces;

namespace Eshava.Storm.Models
{
	internal class Property
	{
		public string Prefix { get; set; }
		public PropertyInfo PropertyInfo { get; set; }
		public object Entity { get; set; }
		public ITypeHandler TypeHandler { get; set; }
	}
}