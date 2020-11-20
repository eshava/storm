using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Administration.BasicInformation
{
	[Table("tbl_Umsatzsteuersaetze")]
    public class VATRateModel 
    {
        private static readonly int _hashCode = Guid.Parse("cf606170-f820-47aa-85f2-a40ec5bfe955").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("UmsatzsteuersatzID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Umsatzsteuersatz", TypeName = "nvarchar")]
        public string VATRateName { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("BerichtLabel", TypeName = "nvarchar")]
        public string ReportLabel { get; set; }
        
        [Required]
        [Range(typeof(decimal), "0","1")]
        [Column("Steuersatz", TypeName = "decimal")]
        public decimal VATRate { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as VATRateModel;

            return model != null && Id.Equals(model.Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return $"{VATRateName} ({VATRate * 100}%)";
        }
    }
}