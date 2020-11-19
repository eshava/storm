using System;
using System.ComponentModel.DataAnnotations;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleAccountingModel
	{
		[DataType(DataType.Date)]
		public DateTime? PurchaseDate { get; set; }

		[MaxLength(50)]
		public string VehicleInventoryNumber { get; set; }
		[MaxLength(50)]
		public string VehicleInvoiceNumber { get; set; }
		[MaxLength(50)]
		public string VehicleExternalOrderNumber { get; set; }

		[MaxLength(50)]
		public string PlatformInventoryNumber { get; set; }
		[MaxLength(50)]
		public string PlatformInvoiceNumber { get; set; }
		[MaxLength(50)]
		public string PlatformExternalOrderNumber { get; set; }

		[MaxLength(50)]
		public string SpecialSuperstructureInventoryNumber { get; set; }
		[MaxLength(50)]
		public string SpecialSuperstructureInvoiceNumber { get; set; }
		[MaxLength(50)]
		public string SpecialSuperstructureExternalOrderNumber { get; set; }

		[Required]
		public Guid? CostCenterId { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ActivationAt { get; set; }

		[Range(0, 100)]
		public int DepreciationForWearAndTearPeriodInMonths { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DepreciationForWearAndTearPeriodEndAt { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal DepreciationForWearAndTearPriceByMonth { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal InsuranceAmountByYear { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal TaxAmountByYear { get; set; }
				
		[Range(typeof(decimal), "0", "10000")]
		public decimal ReferenceFuelConsumption { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal ReferenceFuelPriceByLitre { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal TireCost { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal RepairCost { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal AccidentCost { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal TransitDamagesCost { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal CarWashCost { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal VehicleRent { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal VehicleTrackingCost { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal LoadSecuringCost { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal OtherVehicleCost { get; set; }

		public CostCenterModel CostCenter { get; set; }
	}
}
