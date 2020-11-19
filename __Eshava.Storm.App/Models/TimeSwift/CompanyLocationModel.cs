using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Companies
{
	public class CompanyLocationModel : EquatableObject<CompanyLocationModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("9d0044ad-ea37-407e-b1bd-7185e55f224a").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid CompanyId { get; set; }
		
		public List<CompanyCostUnitModel> CompanyCostUnits { get; set; }
	}
}