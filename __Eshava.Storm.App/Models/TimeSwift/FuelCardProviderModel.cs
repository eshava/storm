using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class FuelCardProviderModel : EquatableObject<FuelCardProviderModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("0534ccd9-ce59-4650-87b1-19963554b61c").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Provider { get; set; }
	}
}