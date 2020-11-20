using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeSalaryComponentModel : EquatableObject<EmployeeSalaryComponentModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("2fb709be-9863-4643-a0b6-bc11d464ceec").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		public Guid EmployeeId { get; set; }

		public Guid SalaryComponentId { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal Value { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime ValidFrom { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ValidTo { get; set; }

		[MaxLength(300)]
		[DataType(DataType.MultilineText)]
		public string Remarks { get; set; }

		public SalaryComponentModel SalaryComponent { get; set; }
		public EmployeeModel Employee { get; set; }
	}
}