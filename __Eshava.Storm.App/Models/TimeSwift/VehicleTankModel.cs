using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleTankModel : EquatableObject<VehicleTankModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("28468571-f6e1-46e8-bad4-59d01b1caa6f").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[Range(0, 5000)]
		public int TankSize { get; set; }

		[Required]
		
		public Guid VehicleId { get; set; }

		[Required]
		
		public Guid FuelTypeId { get; set; }

		public FuelTypeModel FuelType { get; set; }
	}
}