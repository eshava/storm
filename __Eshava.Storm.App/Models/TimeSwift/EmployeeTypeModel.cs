using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeTypeModel : EquatableObject<EmployeeTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("86ef2182-d4ec-43ae-bc2a-f1722d5ac0ed").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(150)]
		public string TypeName { get; set; }
	}
}