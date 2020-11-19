using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class FuelCardModel : EquatableObject<FuelCardModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("fe147a54-d36e-456e-8ca2-628337dc0f39").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string FuelCardNumber { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime ValidTo { get; set; }

		[Required]
		public Guid ProviderId { get; set; }

		public FuelCardProviderModel Provider { get; set; }
	}
}