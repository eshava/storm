using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	[Table("tbl_Countries")]
	public class CountryModel : EquatableObject<CountryModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("e8140e95-5d7b-40d4-836f-cf9b5f4dee4c").GetHashCode();
		protected override int HashCode => _hashCode;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Country { get; set; }

		[Required]
		[MaxLength(2)]
		public string CountryCode { get; set; }
	}
}