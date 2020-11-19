using System;
using System.ComponentModel.DataAnnotations;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleTelematicsModel
	{
		public Guid? TelematicBoxId { get; set; }

		[MaxLength(50)]
		public string BoxSerialNumber { get; set; }

		/// <summary>
		/// E.g. TomTom WebFleet vehicle id
		/// </summary>
		[MaxLength(50)]
		public string ExternalId { get; set; }

		[MaxLength(50)]
		public string FuelBoxName { get; set; }

		[MaxLength(50)]
		public string FuelBoxSerialNumber { get; set; }

		/// <summary>
		/// TomTom WebFleet Account (GPS tracking)
		/// </summary>
		public bool AutomaticDriverLoginWebFleet { get; set; }

		public TelematicBoxModel TelematicBox { get; set; }
	}
}