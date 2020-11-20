using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class SalaryComponentModel : EquatableObject<SalaryComponentModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("ef351668-b384-48f5-a3b5-ce92b1991ff3").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal Value { get; set; }

		/* Open questions */

		// ToDo: What properties there are for this model?
	}
}