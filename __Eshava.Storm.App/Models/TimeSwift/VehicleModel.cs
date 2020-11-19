using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshava.Storm.Attributes;
using TimeSwift.Models.Data.BasicInformation.Companies;
using TimeSwift.Models.Data.BasicInformation.Tours;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	[Table("tbl_Vehicles")]
	public class VehicleModel
	{
		public VehicleModel()
		{
			Financing = new VehicleFinancingModel();
			Accounting = new VehicleAccountingModel();
			Maintenance = new VehicleMaintenanceModel();
			Telematics = new VehicleTelematicsModel();
			PlatformDimension = new DimensionModel();
			VehicleDimension = new DimensionModel();
			Liftgate = new VehicleLiftgateModel();
			Platform = new VehiclePlatformModel();
			SpecialSuperstructure = new VehicleSpecialSuperstructureModel();
			Basics = new VehicleBasicsModel();
		}

		public Guid? Id { get; set; }
		
		/// <summary>
		/// Remove because of tools and materials?
		/// </summary>
		[Range(0, Int32.MaxValue)]
		public int NumberOfCarts { get; set; }

		/// <summary>
		/// Remove because of tools and materials?
		/// </summary>
		[Range(0, Int32.MaxValue)]
		public int NumberOfEuroPallets { get; set; }

		[Required]
		public Guid VehicleOwnerId { get; set; }

		public Guid? StandardTourDefinitionId { get; set; }

		[Required]
		[MaxLength(10)]
		public string RegistrationNumber { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime FirstRegistrationAt { get; set; }

		[MaxLength(17)]
		public string VehicleIdentificationNumber { get; set; }

		[MaxLength(50)]
		public string InsuranceNumber { get; set; }

		[MaxLength(300)]
		[DataType(DataType.MultilineText)]
		public string Remarks { get; set; }

		public Guid? VehicleBrandingId { get; set; }

		/// <summary>
		/// This field corresponds to the date of the first registration with the current owner (only relevant for used vehicles if the first registration date is in the past).
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime ValidFrom { get; set; }

		/// <summary>
		/// This field corresponds to the date of the deregistration or departure from the fleet pool.
		/// </summary>
		[DataType(DataType.Date)]
		public DateTime? ValidTo { get; set; }

		public bool IsTractor { get; set; }


		public VehicleOwnerModel VehicleOwner { get; set; }
		public List<VehicleCompanyLocationModel> VehicleCompanyLocations { get; set; }
		public TourDefinitionModel StandardTourDefinition { get; set; }
		public CompanyModel VehicleBranding { get; set; }
		public List<VehicleTankModel> VehicleTanks { get; set; }
		public List<VehicleCostModel> VehicleCosts { get; set; }
		public List<VehicleDocumentModel> Documents { get; set; }
		public List<VehicleTireModel> VehicleTires { get; set; }

		[OwnsOne]
		public VehicleBasicsModel Basics { get; set; }
		[OwnsOne]
		public VehicleFinancingModel Financing { get; set; }
		[OwnsOne]
		public VehicleAccountingModel Accounting { get; set; }
		[OwnsOne]
		public VehicleMaintenanceModel Maintenance { get; set; }
		[OwnsOne]
		public VehicleTelematicsModel Telematics { get; set; }
		[OwnsOne]
		public VehicleLiftgateModel Liftgate { get; set; }
		[OwnsOne]
		public VehiclePlatformModel Platform { get; set; }
		[OwnsOne]
		public VehicleSpecialSuperstructureModel SpecialSuperstructure { get; set; }
		[OwnsOne]
		public DimensionModel VehicleDimension { get; set; }
		[OwnsOne]
		public DimensionModel PlatformDimension { get; set; }
	}
}