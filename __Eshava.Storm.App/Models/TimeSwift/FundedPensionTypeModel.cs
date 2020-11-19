using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class FundedPensionTypeModel : EquatableObject<FundedPensionTypeModel>, IIdentifier
	{
		private static readonly int _hashcode = Guid.Parse("148f60e7-9c06-4eaa-a28e-59d218cc7abd").GetHashCode();

		protected override int HashCode => _hashcode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }
	}
}