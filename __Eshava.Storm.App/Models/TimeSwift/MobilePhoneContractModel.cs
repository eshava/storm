using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class MobilePhoneContractModel : EquatableObject<MobilePhoneContractModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("3b16b261-3e7a-47fa-bd9c-97a05f9bea2f").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		
		public Guid TariffId { get; set; }

		[Required]
		[MaxLength(20)]
		public string PhoneNumber { get; set; }

		[Required]
		
		public Guid ProviderId { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime ContractEndAt { get; set; }

		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal TariffCostByMonth { get; set; }

		public MobilePhoneProviderModel Provider { get; set; }
		public MobilePhoneTariffModel Tariff { get; set; }
	}
}
