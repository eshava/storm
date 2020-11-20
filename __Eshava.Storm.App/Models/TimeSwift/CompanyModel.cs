using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Companies
{
	public class CompanyModel : EquatableObject<CompanyModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("bf5d7946-fa3d-44f6-8ed0-14e6f4bcf2f9").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string ContactFirstName { get; set; }
		[Required]
		public string ContactLastName { get; set; }

		public bool IsCustomer { get; set; }
		public bool IsSupplier { get; set; }

		public List<CompanyLocationModel> CompanyLocations { get; set; }
	}
}