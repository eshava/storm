using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class CostCenterModel : EquatableObject<CostCenterModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("b982ddc0-b23b-453e-a608-86ddad89adf5").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string CostCenter { get; set; }

		[NotMapped]
		public bool IsUsed { get; set; }
	}
}