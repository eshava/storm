using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Enums;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleFinancingModel
	{
		public FinancingType FinancingType { get; set; }
		public Guid? FinancialInstitutionId { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal FinancingValue { get; set; }

		[DataType(DataType.Date)]
		public DateTime? StartedAt { get; set; }

		[Range(0, 1000)]
		public int RuntimeInMonth { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal FinancingRateMonthly { get; set; }

		[MaxLength(50)]
		public string LoanContractNumber { get; set; }

		[Range(0, 999999)]
		public int GeneralLedgerAccountNumber { get; set; }

		public FinancialInstitutionModel FinancialInstitution { get; set; }
	}
}
