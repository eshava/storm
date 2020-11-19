using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class TireModel : EquatableObject<TireModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("31a075a5-8f6c-4e0d-ae86-0b57f0b5b743").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TireName { get; set; }

		[Required]
		public Guid? VehicleModelId { get; set; }

		public VehicleModelModel VehicleModel { get; set; }
	}
}