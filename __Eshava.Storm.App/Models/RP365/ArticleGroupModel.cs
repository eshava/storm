using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Base.ArticleManagement
{
	[Table("tbl_Artikelgruppen")]
    public class ArticleGroupModel 
    {
        private static readonly int _hashCode = Guid.Parse("0fc9e119-aae1-41a7-bb47-4c46d5a8bf1e").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ArtikelgruppeID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Artikelgruppe", TypeName = "nvarchar")]
        public string GroupName { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj as ArticleGroupModel) == null)
            {
                return false;
            }

            return Equals(Id, ((ArticleGroupModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return GroupName;
        }
    }
}