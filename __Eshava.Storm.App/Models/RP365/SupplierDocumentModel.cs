using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshava.RP365.Models.Data.Administration.BasicInformation;
using Eshava.RP365.Models.Data.Base.BusinessPartnerManagement;
using Eshava.RP365.Models.Data.Enums;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.PurchaseManagement
{
	[Table("tbl_Lieferantendokumente")]
    public class SupplierDocumentModel 
    {
        private static readonly int _hashCode = Guid.Parse("627e3021-c929-4b15-a91d-73cca14d2b1f").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LieferantendokumentId", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Column("DatentypId", TypeName = "int")]
        public Enums.RP365DataType DataType { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Dokumentname", TypeName = "nvarchar")]
        public string DocumentName { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
        [Column("Dokumentdatum", TypeName = "datetime")]
        public DateTime DocumentDate { get; set; }

        [MaxLength(50)]
        [Column("DokumentNr", TypeName = "nvarchar")]
        public string DocumentNumber { get; set; }
      
        [Column("DokumentVerbucht", TypeName = "bit")]
        public bool IsDocumentBooked { get; set; }
        
        [Column("DokumentStatusId", TypeName = "int")]
        public DocumentStatus DocumentStatus { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
        [Column("DokumentStatusDatum", TypeName = "datetime")]
        public DateTime DocumentStatusDate { get; set; }

        [MaxLength(50)]
        [Column("BestellNr", TypeName = "nvarchar")]
        public string OrderNumber { get; set; }

        [Required]
        [Column("UnternehmenId", TypeName = "uniqueidentifier")]
        public Guid CompanyId { get; set; }

        [Required]
        [Column("AnschriftId", TypeName = "uniqueidentifier")]
        public Guid AddressId { get; set; }

        [MaxLength(200)]
        [Column("Ansprechpartner", TypeName = "nvarchar")]
        public string Contact { get; set; }

        [MaxLength(50)]
        [Column("LieferantenNr", TypeName = "nvarchar")]
        public string SupplierNumber { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("A_Name1", TypeName = "nvarchar")]
        public string AddressName1 { get; set; }

        [MaxLength(100)]
        [Column("A_Name2", TypeName = "nvarchar")]
        public string AddressName2 { get; set; }

        [MaxLength(200)]
        [Column("A_Strasse", TypeName = "nvarchar")]
        public string AddressStreet { get; set; }

        [MaxLength(10)]
        [Column("A_Postleitzahl", TypeName = "nvarchar")]
        public string AddressZipCode { get; set; }

        [MaxLength(100)]
        [Column("A_Ort", TypeName = "nvarchar")]
        public string AddressPlace { get; set; }

        [Column("A_LandId", TypeName = "uniqueidentifier")]
        public Guid? AddressCountryId { get; set; }

        [Column("Zahlungskondition", TypeName = "nvarchar(max)")]
        public string PaymentCondition { get; set; }

        [Column("Zahlungsmodalitaet", TypeName = "nvarchar(max)")]
        public string PaymentMethod { get; set; }

        [Required]
        [Column("WaehrungId", TypeName = "uniqueidentifier")]
        public Guid CurrencyId { get; set; }

        [Column("FaktorZurHauswaehrung", TypeName = "decimal")]
        public decimal FactorToApplicationCurrency { get; set; }

        [Column("NummernkreisElementId", TypeName = "uniqueidentifier")]
        public Guid? NumberRangeElementId { get; set; }

        [Column("Tags", TypeName = "nvarchar(max)")]
        public string Tags { get; set; }

        [ForeignKey(nameof(SupplierDocumentPositionModel.SupplierDocumentId))]
        public List<SupplierDocumentPositionModel> Positions { get; set; }

        [ForeignKey(nameof(CurrencyId))]
        public CurrencyModel Currency { get; set; }

        [ForeignKey(nameof(AddressCountryId))]
        public CountryModel AddressCountry { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public CompanyModel Company { get; set; }

        [ForeignKey(nameof(AddressId))]
        public AddressModel Address { get; set; }

        [MaxLength(50)]
        [Column("ExterneReferenzNr", TypeName = "nvarchar")]
        public string ExternalReferenceNumber { get; set; }
        
        public override bool Equals(object obj)
        {
            if ((obj as SupplierDocumentModel) == null)
            {
                return false;
            }

            return Equals(Id, ((SupplierDocumentModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return $"{DocumentNumber}: {DocumentName}";
        }

		public string DocumentNumberAndIndex => DocumentNumber;
    }
}