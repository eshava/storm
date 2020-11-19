using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeVehicleLicenceClassModel : EquatableObject<EmployeeVehicleLicenceClassModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("e411fd6a-3a20-4d94-a6bb-07560f83fe23").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid EmployeeId { get; set; }

		[Required]
		public Guid VehicleLicenceClassId { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ValidFrom { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime ExpiredAt { get; set; }

		public VehicleLicenceClassModel VehicleLicenceClass { get; set; }
		public EmployeeModel Employee { get; set; }
	}
}