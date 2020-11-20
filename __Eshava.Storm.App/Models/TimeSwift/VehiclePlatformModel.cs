using System;
using System.ComponentModel.DataAnnotations;


namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehiclePlatformModel
	{
		
		public Guid? ManufacturerId { get; set; }

		
		public Guid? TypeId { get; set; }

		[MaxLength(20)]
		public string SerialNumber { get; set; }

		public bool HasSideDoor { get; set; }

		public PlatformManufacturerModel Manufacturer { get; set; }
		public PlatformTypeModel Type { get; set; }
	}
}