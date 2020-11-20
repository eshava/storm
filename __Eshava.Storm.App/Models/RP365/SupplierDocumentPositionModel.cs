using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshava.RP365.Models.Data.Administration.BasicInformation;
using Eshava.RP365.Models.Data.Base.ArticleManagement;
using Eshava.RP365.Models.Data.Enums;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.PurchaseManagement
{
	[Table("tbl_LieferantendokumentePositionen")]
    public class SupplierDocumentPositionModel
    {
        private static readonly int _hashCode = Guid.Parse("b5cad1ef-a94b-45f6-8289-1ddbde9a6564").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LieferantendokumentPositionId", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Column("LieferantendokumentId", TypeName = "uniqueidentifier")]
        public Guid? SupplierDocumentId { get; set; }

        [Column("PositionstypId", TypeName = "int")]
        public PositionType PositionType { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("SequenzNr", TypeName = "int")]
        public int SequenceNumber { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("PositionsNr", TypeName = "int")]
        public int PositionNumber { get; set; }

        [MaxLength(200)]
        [Column("Benennung", TypeName = "nvarchar")]
        public string PositionName { get; set; }

        [Column("Beschreibung", TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Required]
        [Column("Menge", TypeName = "decimal")]
        public decimal Amount { get; set; }

        [Required]
        [Column("Einzelpreis", TypeName = "decimal")]
        public decimal SinglePrice { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("Preiseinheit", TypeName = "int")]
        public int PriceUnit { get; set; }

        [Required]
        [Column("MengeneinheitId", TypeName = "uniqueidentifier")]
        public Guid QuantityUnitId { get; set; }

        [MaxLength(50)]
        [Column("ArtikelNr", TypeName = "nvarchar")]
        public string ArticleNumber { get; set; }

        [Column("ArtikelId", TypeName = "uniqueidentifier")]
        public Guid? ArticleId { get; set; }

        [Column("UmsatzsteuersatzId", TypeName = "uniqueidentifier")]
        public Guid? VATRateId { get; set; }

        [Column("PositionsrabattBetrag", TypeName = "decimal")]
        public decimal PositionDiscountValue { get; set; }

        [Column("Tags", TypeName = "nvarchar(max)")]
        public string Tags { get; set; }

        [ForeignKey(nameof(VATRateId))]
        public VATRateModel VATRate { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public ArticleModel Article { get; set; }

        [ForeignKey(nameof(QuantityUnitId))]
        public QuantityUnitModel QuantityUnit { get; set; }

		[ForeignKey(nameof(SupplierDocumentId))]
		public SupplierDocumentModel Document { get; set; }

		[NotMapped]
		public Enums.PositionStatus PositionStatus { get { return Enums.PositionStatus.Booked; } }

		[NotMapped]
        public bool IsOptional => false;

        [Column("PositionswertNetto", TypeName = "decimal")]
        public decimal PositionValueNet { get; set; }

        [Column("PositionswertUst", TypeName = "decimal")]
        public decimal PositionValueVAT { get; set; }

        [Column("PositionswertBrutto", TypeName = "decimal")]
        public decimal PositionValueGross { get; set; }

        [Column("PositionswertManuell", TypeName = "bit")]
        public bool IsPositionValueManualInput { get; set; }
        
        public override bool Equals(object obj)
        {
            if ((obj as SupplierDocumentPositionModel) == null)
            {
                return false;
            }

            return Equals(Id, ((SupplierDocumentPositionModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return $"{PositionNumber}: {PositionName}";
        }
    }
}