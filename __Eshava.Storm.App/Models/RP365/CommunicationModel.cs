using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshava.RP365.Models.Data.Base.BusinessPartnerManagement
{
	[Table("tbl_Adressverwaltung_Kommunikationen")]
    public class CommunicationModel
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("KommunikationID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Required]
        [Column("LinkId", TypeName = "uniqueidentifier")]
        public Guid LinkId { get; set; }

        [Required]
        [Column("KommunikationsartID", TypeName = "uniqueidentifier")]
        public Guid CommunicationTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Kommunikation", TypeName = "nvarchar")]
        public string Communication { get; set; }

        [MaxLength(100)]
        [Column("Beschreibung", TypeName = "nvarchar")]
        public string CommunicationDescription { get; set; }
    }
}