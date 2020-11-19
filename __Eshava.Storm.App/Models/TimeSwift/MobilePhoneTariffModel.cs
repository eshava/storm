using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class MobilePhoneTariffModel : EquatableObject<MobilePhoneTariffModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("5e9d529d-3934-4391-9f91-cdd2792f0560").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string TariffName { get; set; }

		[Required]
		
		public Guid ProviderId { get; set; }
		
		public MobilePhoneProviderModel Provider { get; set; }
	}
}