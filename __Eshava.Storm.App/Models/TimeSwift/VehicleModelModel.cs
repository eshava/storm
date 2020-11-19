using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleModelModel : EquatableObject<VehicleModelModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("2e474c92-d44b-4676-ab8d-c6006d3b72f5").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		
		public Guid? VehicleManufacturerId { get; set; }

		[Required]
		[MaxLength(50)]
		public string ModelName { get; set; }
				
		public VehicleManufacturerModel VehicleManufacturer { get; set; }
	}
}