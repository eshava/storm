using System;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleListItem
	{
		public Guid Id { get; set; }
		public string RegistrationNumber { get; set; }
		public DateTime? ValidTo { get; set; }
	}
}