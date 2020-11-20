using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class FinancialInstitutionModel : EquatableObject<FinancialInstitutionModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("cb2054d0-1e5b-46df-9db2-09cf47c0722f").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string BankName { get; set; }
	}
}