using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class QualificationModel : EquatableObject<QualificationModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("e57e1a52-df55-4080-a150-7cee9a204a5f").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string QualificationName { get; set; }

		public bool CanExpire { get; set; }

		public bool IsOtherQualification { get; set; }
	}
}