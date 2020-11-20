using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Tours
{
	public class PriceKeyFactorModel : EquatableObject<PriceKeyFactorModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("b0713ee9-bd3f-4548-b07e-10f4f2f2903b").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(20)]
		public string KeyFactorName { get; set; }

		[Required]
		[MaxLength(20)]
		public string KeyFactorUnit { get; set; }
	}
}