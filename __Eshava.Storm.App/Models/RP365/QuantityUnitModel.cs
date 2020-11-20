using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Administration.BasicInformation
{
	[Table("tbl_Mengeneinheiten")]
    public class QuantityUnitModel 
    {
        private static readonly int _hashCode = Guid.Parse("c73cc754-13a8-4d36-87c2-0b432e364810").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MengeneinheitID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Required]
        [Column("MengeneinheitGruppeID", TypeName = "uniqueidentifier")]
        public Guid GroupId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Mengeneinheit", TypeName = "nvarchar")]
        public string QuantityUnit { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("FaktorEinheit", TypeName = "int")]
        public int FactorUnit { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("FaktorGruppe", TypeName = "int")]
        public int FactorGroup { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("Sortierung", TypeName = "int")]
        public int Sorting { get; set; }

        [DataType(DataType.MultilineText)]
        [Column("Bemerkung", TypeName = "nvarchar(max)")]
        public string Remark { get; set; }

        [Column("Standard", TypeName = "bit")]
        public bool IsDefault { get; set; }

        [Required]
        [Range(0, 6)]
        [Column("AnzahlNachkommastellen", TypeName = "int")]
        public int NumberOfDecimalPlaces { get; set;}

        [JsonIgnore]
        [ForeignKey(nameof(GroupId))]
        public QuantityUnitGroupModel Group { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj as QuantityUnitModel) == null)
            {
                return false;
            }

            return Id.Equals(((QuantityUnitModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return QuantityUnit;
        }
    }
}