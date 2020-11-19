using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class ToolsAndMaterialsTypeModel : EquatableObject<ToolsAndMaterialsTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("a6af0dcb-4e9d-4111-8966-70094d5e6b8d").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }
	}
}