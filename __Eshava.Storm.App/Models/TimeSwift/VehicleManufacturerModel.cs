using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleManufacturerModel : EquatableObject<VehicleManufacturerModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("64420051-6961-450b-968b-e8dcbc25d776").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Manufacturer { get; set; }
	}
}