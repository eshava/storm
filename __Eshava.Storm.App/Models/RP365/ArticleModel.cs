using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshava.RP365.Models.Data.Administration.BasicInformation;

namespace Eshava.RP365.Models.Data.Base.ArticleManagement
{
	[Table("tbl_Artikel")]
	public class ArticleModel 
	{
		private static readonly int _hashCode = Guid.Parse("65801492-93b4-4f85-a376-0bb1ce7f12c0").GetHashCode();
				
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ArtikelID", TypeName = "uniqueidentifier")]
		public Guid? Id { get; set; }

		[MaxLength(50)]
		[Column("ArtikelNr", TypeName = "nvarchar")]
		public string ArticleNumber { get; set; }

		[Required]
		[Column("ArtikelgruppeID", TypeName = "uniqueidentifier")]
		public Guid ArticleGroupId { get; set; }

		[Required]
		[MaxLength(50)]
		[Column("Bezeichnung", TypeName = "nvarchar")]
		public string ArticleName { get; set; }

		[Column("Beschreibung", TypeName = "nvarchar(max)")]
		public string Description { get; set; }

		[Required]
		[Range(typeof(decimal), "0", "10000000")]
		[Column("EinkaufPreis", TypeName = "decimal")]
		public decimal PurchasePrice { get; set; }

		[Required]
		[Range(1, Int32.MaxValue)]
		[Column("EinkaufPreiseinheit", TypeName = "int")]
		public int PurchasePriceUnit { get; set; }

		[Required]
		[Column("EinkaufMengeneinheitID", TypeName = "uniqueidentifier")]
		public Guid PurchaseQuantityUnitId { get; set; }

		[Required]
		[Column("EinkaufWaehrungID", TypeName = "uniqueidentifier")]
		public Guid PurchaseCurrencyId { get; set; }

		[Required]
		[Range(typeof(decimal), "0", "10000000")]
		[Column("VerkaufPreis", TypeName = "decimal")]
		public decimal SalesPrice { get; set; }

		[Required]
		[Range(1, Int32.MaxValue)]
		[Column("VerkaufPreiseinheit", TypeName = "int")]
		public int SalesPriceUnit { get; set; }

		[Required]
		[Column("VerkaufMengeneinheitID", TypeName = "uniqueidentifier")]
		public Guid SalesQuantityUnitId { get; set; }

		[Required]
		[Column("VerkaufWaehrungID", TypeName = "uniqueidentifier")]
		public Guid SalesCurrencyId { get; set; }

		[Required]
		[Column("UmsatzsteuersatzID", TypeName = "uniqueidentifier")]
		public Guid VATRateId { get; set; }

		[Column("Aktiv", TypeName = "bit")]
		public bool IsActive { get; set; }

		[Column("Tags", TypeName = "nvarchar(max)")]
		public string Tags { get; set; }

		[ForeignKey(nameof(ArticleGroupId))]
		public ArticleGroupModel ArticleGroup { get; set; }

		[ForeignKey(nameof(VATRateId))]
		public VATRateModel VATRate { get; set; }

		[ForeignKey(nameof(PurchaseCurrencyId))]
		public CurrencyModel PurchaseCurrency { get; set; }

		[ForeignKey(nameof(SalesCurrencyId))]
		public CurrencyModel SalesCurrency { get; set; }

		[ForeignKey(nameof(PurchaseQuantityUnitId))]
		public QuantityUnitModel PurchaseQuantityUnit { get; set; }

		[ForeignKey(nameof(SalesQuantityUnitId))]
		public QuantityUnitModel SalesQuantityUnit { get; set; }

		public override bool Equals(object obj)
		{
			if ((obj as ArticleModel) == null)
			{
				return false;
			}

			return Equals(Id, ((ArticleModel)obj).Id);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override string ToString()
		{
			return $"{ArticleNumber}: {ArticleName}";
		}
	}
}