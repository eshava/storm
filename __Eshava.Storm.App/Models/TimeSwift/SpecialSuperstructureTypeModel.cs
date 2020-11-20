using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class SpecialSuperstructureTypeModel : EquatableObject<SpecialSuperstructureTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("6b3e9522-2ad3-432d-9528-46a1547b5790").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid? ManufacturerId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		public SpecialSuperstructureManufacturerModel Manufacturer { get; set; }
	}
}