using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeePersonalEquipmentModel : EquatableObject<EmployeePersonalEquipmentModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("8FDD8557-2797-47C8-A4D4-8EC8BE47535A").GetHashCode();

		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		public Guid EmployeeId { get; set; }

		public Guid EquipmentTypeId { get; set; }

		[MaxLength(150)]
		public string Description { get; set; }

		/// <summary>
		/// ToDo: Rename column to ClothingTypeId
		/// </summary>
		public Guid? ClothTypeId { get; set; }

		[MaxLength(3)]
		public string Size { get; set; }

		[Range(1, 10)]
		public int Received { get; set; }

		public ClothingTypeModel ClothingType { get; set; }
		public EquipmentTypeModel EquipmentType { get; set; }
		public EmployeeModel Employee { get; set; }
	}
}