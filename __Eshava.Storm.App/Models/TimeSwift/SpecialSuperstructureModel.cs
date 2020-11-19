using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class SpecialSuperstructureModel : EquatableObject<SpecialSuperstructureModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("2acc71c2-1889-4f55-81ae-f1b0d1edf0ca").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid? ManufacturerId { get; set; }

		[Required]
		public Guid? TypeId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		public SpecialSuperstructureManufacturerModel Manufacturer { get; set; }
		public SpecialSuperstructureTypeModel Type { get; set; }
	}
}