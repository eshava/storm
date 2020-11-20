using System;
using System.ComponentModel.DataAnnotations;


namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleMaintenanceModel
	{
		[DataType(DataType.Date)]
		public DateTime? ContractStartAt { get; set; }

		
		public Guid? ContractTypeId { get; set; }

		
		public Guid? ContractPeriodId { get; set; }

		[Range(0, Int32.MaxValue)]
		public int ContractKilometersPerYear { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal ContractCostPerMonth { get; set; }

		public MaintenanceContractTypeModel ContractType { get; set; }
		public TimeIntervalModel ContractPeriod { get; set; }
	}
}