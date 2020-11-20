using System;
using System.ComponentModel.DataAnnotations;


namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleSpecialSuperstructureModel
	{
		
		public Guid? ManufacturerId { get; set; }

		
		public Guid? TypeId { get; set; }

		
		public Guid? NameId { get; set; }

		[MaxLength(50)]
		public string SerialNumber { get; set; }

		public SpecialSuperstructureManufacturerModel Manufacturer { get; set; }
		public SpecialSuperstructureTypeModel Type { get; set; }
		public SpecialSuperstructureModel Name { get; set; }
	}
}
