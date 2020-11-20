using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class WorkActivityUsageCategoryModel : EquatableObject<WorkActivityUsageCategoryModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("daeafe06-e34d-4502-a5be-328b313839a2").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string UsageCategoryName { get; set; }
	}
}