using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Administration.BasicInformation
{
	[Table("tbl_Waehrungen")]
    public class CurrencyModel 
    {
        private static readonly int _hashCode = Guid.Parse("403fd0be-3ade-4261-9bd5-091ee0d6e1fb").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WaehrungID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Waehrung", TypeName = "nvarchar")]
        public string CurrencyName { get; set; }

        [Required]
        [MaxLength(5)]
        [Column("Waehrungscode", TypeName = "nvarchar")]
        public string CurrencyCode { get; set; }

        [Required]
        [MaxLength(5)]
        [Column("Waehrungssymbol", TypeName = "nvarchar")]
        public string CurrencySymbol { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "10000000")]
        [Column("FaktorZurHauswaehrung", TypeName = "decimal")]
        public decimal FactorToApplicationCurrency { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("AktualisiertAm", TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public string DisplayName => ToString();

        public override bool Equals(object obj)
        {
            if ((obj as CurrencyModel) == null) {
                return false;
            }

            return Equals(Id,((CurrencyModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return $"{CurrencyName} ({CurrencyCode})";
        }
    }
}