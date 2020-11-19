using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class PlatformManufacturerModel : EquatableObject<PlatformManufacturerModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("6300e70a-2d47-40f7-b40e-cbe6e4c3cacc").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Manufacturer { get; set; }
	}
}