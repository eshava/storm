using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class WorkActivityTypeModel : EquatableObject<WorkActivityTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("f8a28439-e2e5-4203-8189-30857bd28df8").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }
	}
}