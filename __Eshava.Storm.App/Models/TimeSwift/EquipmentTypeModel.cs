using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EquipmentTypeModel : EquatableObject<EquipmentTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("700EFD4B-FDAC-46F7-8FEB-F6225EF536C2").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }
	}
}