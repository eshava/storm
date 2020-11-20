using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Administration.BasicInformation
{
	[Table("tbl_Laender")]
    public class CountryModel 
    {
        private static readonly int _hashCode = Guid.Parse("cf347738-7036-4e3b-821f-c84d196b6a81").GetHashCode();

        
        [Key]
        [Column("LandID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Land", TypeName = "nvarchar")]
        public string CountryName { get; set; }

        [Required]
        [MaxLength(5)]
        [Column("Laendercode", TypeName = "nvarchar")]
        public string CountryCode { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("Vorwahl", TypeName = "nvarchar")]
        public string AreaCode { get; set; }

        [Required]
        [MaxLength(5)]
        [Column("PLZ_Praefix", TypeName = "nvarchar")]
        public string ZipCodePrefix { get; set; }

        public override string ToString()
        {
            return CountryName;
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override bool Equals(object obj)
        {
            var model = obj as CountryModel;

            return model != null && Id.Equals(model.Id);
        }
    }
}