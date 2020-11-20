using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleTypeModel : EquatableObject<VehicleTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("2c790169-ab2a-4594-bb68-28a4c129d1ca").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }

		/// <summary>
		/// TomTom WebFleet Account (GPS tracking)
		/// </summary>
		public bool AutomaticDriverLoginWebFleet { get; set; }

	}
}