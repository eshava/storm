using System;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Companies
{
	public class CompanyCostUnitModel : EquatableObject<CompanyCostUnitModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("6d948b3b-cafc-4b5b-bbaf-d2e03a2fdc4f").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		public Guid CompanyLocationId { get; set; }
	}
}