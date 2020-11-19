using System;
using System.ComponentModel.DataAnnotations;


namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleLiftgateModel
	{
		
		public Guid? ManufacturerId { get; set; }

		
		public Guid? TypeId { get; set; }

		[MaxLength(50)]
		public string SerialNumber { get; set; }

		[Range(0, Int32.MaxValue)]
		public int LiftingForceInKilogramme { get; set; }

		public LiftgateTypeModel Type { get; set; }
		public LiftgateManufacturerModel Manufacturer { get; set; }
	}
}