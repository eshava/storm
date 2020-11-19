using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class NationalityModel : EquatableObject<NationalityModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("9afa51da-b5ee-4f63-86aa-0e27a8f29a41").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
	}
}