using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class WorkActivityModel : EquatableObject<WorkActivityModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("366de9fd-9fc0-4d41-b6f1-23acc4137f3c").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string WorkActivityName { get; set; }

		[Required]
		
		public Guid WorkActivityTypeId { get; set; }

		
		public Guid? WorkActivityUsageCategoryId { get; set; }

		public bool IsCompanyCostUnitEnabled { get; set; }

		[Required]
		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal Expenses { get; set; }

		public WorkActivityTypeModel WorkActivityType { get; set; }

		public WorkActivityUsageCategoryModel WorkActivityUsageCategory { get; set; }
	}
}