using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class LiftgateTypeModel : EquatableObject<LiftgateTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("555bb1d6-a4b2-4245-bb53-258a1463aa4e").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string LiftgateType { get; set; }

		[Required]
		public Guid? ManufacturerId { get; set; }

		public LiftgateManufacturerModel Manufacturer { get; set; }
	}
}