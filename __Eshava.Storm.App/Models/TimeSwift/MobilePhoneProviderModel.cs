using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class MobilePhoneProviderModel : EquatableObject<MobilePhoneProviderModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("b3f929c3-419a-4f64-af51-b4a86661855c").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string ProviderName { get; set; }
	}
}