using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeQualificationModel : EquatableObject<EmployeeQualificationModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("79db6f60-0a74-4687-a273-c8227db992e2").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }
		
		[Required]
		public Guid EmployeeId { get; set; }

		[Required]
		public Guid QualificationId { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ValidFrom { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ExpiredAt { get; set; }

		[MaxLength(300)]
		[DataType(DataType.MultilineText)]
		public string Remarks { get; set; }

		public QualificationModel Qualification { get; set; }
		public EmployeeModel Employee { get; set; }
	}
}