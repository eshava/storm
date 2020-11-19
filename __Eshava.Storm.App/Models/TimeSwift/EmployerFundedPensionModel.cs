using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployerFundedPensionModel : EquatableObject<EmployerFundedPensionModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("8995683d-9d47-468c-bf5e-bb7115a4af6d").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid EmployeeId { get; set; }

		[Required]
		public Guid TypeId { get; set; }

		[Required]
		[MaxLength(100)]
		public string ProviderName { get; set; }

		[Required]
		[MaxLength(50)]
		public string InsuranceNumber { get; set; }

		[Required]
		public Guid FinancingTypeId { get; set; }
		
		[Required]
		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal EmployeeAmountByMonth { get; set; }

		[Required]
		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal EmployerAmountByMonth { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime ContractStartAt { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ContractEndAt { get; set; }

		public EmployeeModel Employee { get; set; }
		public FundedPensionTypeModel FundedPensionType { get; set; }
		public FundedPensionFinancingTypeModel FundedPensionFinancingType { get; set; }
	}
}