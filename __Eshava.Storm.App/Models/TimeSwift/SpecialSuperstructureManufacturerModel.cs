

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class SpecialSuperstructureManufacturerModel : EquatableObject<SpecialSuperstructureManufacturerModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("f09bca37-88f8-43af-b69e-d486c639fbe0").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Manufacturer { get; set; }

		public List<SpecialSuperstructureModel> SpecialSuperstructures { get; set; }
		public List<SpecialSuperstructureTypeModel> SpecialSuperstructureTypes { get; set; }
	}
}
