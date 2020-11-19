using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class MaintenanceContractTypeModel : EquatableObject<MaintenanceContractTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("9b7a89f1-0043-493c-b8a7-f6aff79a3dd6").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }

		[Required]
		public Guid? VehicleManufacturerId { get; set; }

		public VehicleManufacturerModel VehicleManufacturer { get; set; }
	}
}