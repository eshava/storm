using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Enums;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleBasicsModel
	{
		
		public Guid? TypeId { get; set; }

		
		public Guid? ManufacturerId { get; set; }

		
		public Guid? ModelId { get; set; }

		[Required]
		[Range(0, Int32.MaxValue)]
		public int Mileage { get; set; }

		[Range(0, Int32.MaxValue)]
		public int HorsePower { get; set; }

		[Range(0, Int32.MaxValue)]
		public int Kilowatt { get; set; }

		[Range(0, Int32.MaxValue)]
		public int CubicCapacityInCubicCentimeter { get; set; }

		[Range(0, Int32.MaxValue)]
		public int EmptyWeightInKilogramme { get; set; }

		[Range(0, Int32.MaxValue)]
		public int GrossVehicleWeightInKilogramme { get; set; }

		[Range(0, Int32.MaxValue)]
		public int MaxAxleLoadFrontAxleInKilogramme { get; set; }

		[Range(0, Int32.MaxValue)]
		public int MaxAxleLoadRearAxleInKilogramme { get; set; }

		[Range(1, 6)]
		public int NumberOfAxles { get; set; }

		[Required]
		public EnvironmentalBadge EnvironmentalBadge { get; set; }

		public Guid? EmissionClassId { get; set; }


		public EmissionClassModel EmissionClass { get; set; }
		public VehicleTypeModel Type { get; set; }
		public VehicleManufacturerModel Manufacturer { get; set; }
		public VehicleModelModel Model { get; set; }
	}
}
