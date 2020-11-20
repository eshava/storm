using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeHolidayEntitlementModel : EquatableObject<EmployeeHolidayEntitlementModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("9c50e8bf-6e9e-4aa3-9d59-977aa8f2901d").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid EmployeeId { get; set; }

		[Range(0, 99)]
		public int HolidayEntitlement { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ValidTo { get; set; }

		public bool ReadOnly { get; set; }
	}
}