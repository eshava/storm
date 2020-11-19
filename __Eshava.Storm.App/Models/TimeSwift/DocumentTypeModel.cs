using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Enums;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class DocumentTypeModel: EquatableObject<DocumentTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("9EACF46B-8A2D-4198-B9EE-CF6D0BCDD159").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		public TimeSwiftDataType DataType { get; set; }
	}
}
