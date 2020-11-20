using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Enums;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class CostTypeModel : EquatableObject<CostTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("c68af404-329f-44cf-85d1-0cad5291ea2f").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(30)]
		public string TypeName { get; set; }

		[Range(1, 2)]
		public TimeSwiftDataType DataType { get; set; }
	}
}