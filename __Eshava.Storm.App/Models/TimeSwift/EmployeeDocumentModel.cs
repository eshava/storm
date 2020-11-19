using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeDocumentModel : EquatableObject<EmployeeDocumentModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("f4fc45b7-6925-4566-bed3-be7c958eacfa").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid EmployeeId { get; set; }
		public EmployeeModel Employee { get; set; }

		[Required]
		[MaxLength(50)]
		public string FileName { get; set; }
		
		[Required]
		public Guid? DocumentTypeId { get; set; }

		public DocumentTypeModel DocumentType { get; set; }
	}
}
