using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Administration.BasicInformation
{
	[Table("tbl_Mengeneinheiten_Gruppen")]
    public class QuantityUnitGroupModel
    {
        private static readonly int _hashCode = Guid.Parse("2d071d3e-14ec-4d16-b76a-79e9e4da2632").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MengeneinheitGruppeID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("MengeneinheitGruppe", TypeName = "nvarchar")]
        public string GroupName { get; set; }

        public override string ToString()
        {
            return GroupName;
        }

        public override bool Equals(object obj)
        {
            return Id.Equals(((QuantityUnitGroupModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}