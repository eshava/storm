using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class ClothingTypeModel : EquatableObject<ClothingTypeModel>, IIdentifier
	{
		private static readonly int _hashcode = Guid.Parse("A2F62EC3-59D5-4F56-904E-A6F6ACB54495").GetHashCode();

		protected override int HashCode => _hashcode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string ClothingName { get; set; }
	}
}
