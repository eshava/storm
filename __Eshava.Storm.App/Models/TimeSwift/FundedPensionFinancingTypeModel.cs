using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class FundedPensionFinancingTypeModel : EquatableObject<FundedPensionFinancingTypeModel>, IIdentifier
	{
		private static readonly int _hashcode = Guid.Parse("8b09af17-6024-4afc-8914-e5dfb2720d6b").GetHashCode();

		protected override int HashCode => _hashcode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }
	}
}