using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleCostModel : EquatableObject<VehicleCostModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("e049a58b-c271-4181-8b8d-1d4ffd534b42").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal Cost { get; set; }

		[Required]
		
		public Guid? VehicleId { get; set; }

		[Required]
		
		public Guid? CostTypeId { get; set; }

		public CostTypeModel CostType { get; set; }
	}
}