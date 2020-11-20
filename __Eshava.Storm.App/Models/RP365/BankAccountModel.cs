using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Base.BusinessPartnerManagement
{
	[Table("tbl_Adressverwaltung_Bankverbindungen")]
	public class BankAccountModel
	{
		private static readonly int _hashCode = Guid.Parse("c86914bc-93cb-4f76-9a18-32cbd5fef410").GetHashCode();

		[NotMapped]
		[JsonIgnore]
		public Guid? ParentId => AddressId;

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Id", TypeName = "uniqueidentifier")]
		public Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		[Column("BankAccountName", TypeName = "nvarchar")]
		public string BankAccountName { get; set; }

		[Required]
		[Column("AddressId", TypeName = "uniqueidentifier")]
		public Guid AddressId { get; set; }

		[Required]
		[MaxLength(34)]
		[JsonProperty("iban")]
		[Column("IBAN", TypeName = "nvarchar")]
		public string IBAN { get; set; }

		[Required]
		[MaxLength(11)]
		[JsonProperty("bic")]
		[Column("BIC", TypeName = "nvarchar")]
		public string BIC { get; set; }

		public override string ToString()
		{
			return $"{IBAN} ({BIC})";
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override bool Equals(object obj)
		{
			if ((obj as BankAccountModel) == null)
			{
				return false;
			}

			return Id.Equals(((BankAccountModel)obj).Id);
		}
	}
}