using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class MobilePhoneModel : EquatableObject<MobilePhoneModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("61c5db56-2932-4d23-adca-649a76d74821").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string ModelName { get; set; }

		[Required]
		[MaxLength(50)]
		public string SerialNumber { get; set; }

		[Required]
		[MaxLength(50)]
		public string EMEI { get; set; }
	}
}