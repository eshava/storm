using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class HealthInsuranceCompanyModel : EquatableObject<HealthInsuranceCompanyModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("2f06d7ef-1b1f-47b2-9360-025a8437db72").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string CompanyName { get; set; }
	}	
}