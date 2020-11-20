using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshava.RP365.Models.Data.Administration.BasicInformation;

namespace Eshava.RP365.Models.Data.Base.BusinessPartnerManagement
{
	[Table("tbl_Adressverwaltung_Anschriften")]
	public class AddressModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("AnschriftID", TypeName = "uniqueidentifier")]
		public Guid? Id { get; set; }

		[Required]
		[Column("UnternehmenID", TypeName = "uniqueidentifier")]
		public Guid CompanyId { get; set; }

		[Column("StandardAnsprechpartnerID", TypeName = "uniqueidentifier")]
		public Guid? DefaultContactId { get; set; }

		[Required]
		[MaxLength(50)]
		[Column("Kurzbezeichnung", TypeName = "nvarchar")]
		public string ShortName { get; set; }

		[Required]
		[MaxLength(100)]
		[Column("Unternehmensname1", TypeName = "nvarchar")]
		public string CompanyName1 { get; set; }

		[MaxLength(100)]
		[Column("Unternehmensname2", TypeName = "nvarchar")]
		public string CompanyName2 { get; set; }

		[MaxLength(200)]
		[Column("Strasse", TypeName = "nvarchar")]
		public string Street { get; set; }

		[MaxLength(10)]
		[Column("Postleitzahl", TypeName = "nvarchar")]
		public string ZipCode { get; set; }

		[MaxLength(100)]
		[Column("Ort", TypeName = "nvarchar")]
		public string Place { get; set; }

		[Column("LandID", TypeName = "uniqueidentifier")]
		public Guid? CountryId { get; set; }

		[Column("Beschreibung", TypeName = "nvarchar(max)")]
		public string Description { get; set; }

		[Column("Aktiv", TypeName = "bit")]
		public bool IsAddressActive { get; set; }

		[MaxLength(200)]
		[Column("Lieferbedingung", TypeName = "nvarchar")]
		public string DeliveryCondition { get; set; }

		[MaxLength(200)]
		[Column("Versandart", TypeName = "nvarchar")]
		public string ShipmentMode { get; set; }

		[Column("Zahlungskondition", TypeName = "nvarchar(max)")]
		public string PaymentConditionString { get; set; }

		[Column("Zahlungsmodalitaet", TypeName = "nvarchar(max)")]
		public string PaymentMethod { get; set; }

		[MaxLength(50)]
		[Column("Steuernummer", TypeName = "nvarchar")]
		public string TaxNumber { get; set; }

		[MaxLength(50)]
		[Column("UmsatzsteuerIDNummer", TypeName = "nvarchar")]
		public string VATidentNumber { get; set; }

		[Required]
		[Column("UmsatzsteuersatzID", TypeName = "uniqueidentifier")]
		public Guid? VATRateId { get; set; }

		[Required]
		[Column("WaehrungID", TypeName = "uniqueidentifier")]
		public Guid? CurrencyId { get; set; }

		[Required]
		[MaxLength(5)]
		[Column("Sprache", TypeName = "nvarchar")]
		public string CompanyCulture { get; set; }

		[Column("NummernkreisID", TypeName = "uniqueidentifier")]
		public Guid? NumberRangeId { get; set; }

		[Column("Kunde", TypeName = "bit")]
		public bool IsCustomer { get; set; }

		[DataType(DataType.Date)]
		[Column("KundeSeit", TypeName = "datetime")]
		public DateTime? CustomerSince { get; set; }

		[Column("Lieferant", TypeName = "bit")]
		public bool IsSupplier { get; set; }

		[DataType(DataType.Date)]
		[Column("LieferantSeit", TypeName = "datetime")]
		public DateTime? SupplierSince { get; set; }

		[ForeignKey(nameof(CommunicationModel.LinkId))]
		public List<CommunicationModel> Communications { get; set; }

		[ForeignKey(nameof(ContactModel.AddressId))]
		public List<ContactModel> Contacts { get; set; }

		[ForeignKey(nameof(BankAccountModel.AddressId))]
		public List<BankAccountModel> BankAccounts { get; set; }

		[ForeignKey(nameof(CompanyId))]
		public CompanyModel Company { get; set; }
				
		[ForeignKey(nameof(VATRateId))]
		public VATRateModel VATRate { get; set; }

		[ForeignKey(nameof(CurrencyId))]
		public CurrencyModel Currency { get; set; }

		[ForeignKey(nameof(CountryId))]
		public CountryModel Country { get; set; }
	}
}