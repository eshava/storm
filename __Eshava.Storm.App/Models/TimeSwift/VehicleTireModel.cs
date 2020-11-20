using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleTireModel : EquatableObject<VehicleTireModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("66324249-cf34-447a-b82f-7a26305681cf").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		
		public Guid VehicleId { get; set; }

		[Required]
		[MaxLength(50)]
		public string AxisName { get; set; }

		[Required]
		public Guid? TireId { get; set; }

		[Range(typeof(decimal), "0", "10")]
		public decimal Pressure { get; set; }

		public int SortOrder { get; set; }

		public TireModel Tire { get; set; }
	}
}