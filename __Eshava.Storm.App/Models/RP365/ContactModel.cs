using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshava.RP365.Models.Data.Enums;
using Newtonsoft.Json;

namespace Eshava.RP365.Models.Data.Base.BusinessPartnerManagement
{
	[Table("tbl_Adressverwaltung_Ansprechpartner")]
    public class ContactModel 
    {
        private static readonly int _hashCode = Guid.Parse("86bed771-596d-4cfe-a9f0-a92d731d585b").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("AnsprechpartnerID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Column("UnternehmenID", TypeName = "uniqueidentifier")]
        public Guid CompanyId { get; set; }

        [Column("AnschriftID", TypeName = "uniqueidentifier")]
        public Guid? AddressId { get; set; }

        [MaxLength(50)]
        [Column("Anrede", TypeName = "nvarchar")]
        public string Title { get; set; }

        [MaxLength(100)]
        [Column("Vorname", TypeName = "nvarchar")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Nachname", TypeName = "nvarchar")]
        public string LastName { get; set; }

        [MaxLength(100)]
        [Column("Namenszusatz", TypeName = "nvarchar")]
        public string NameAffix { get; set; }

        [MaxLength(50)]
        [Column("Position", TypeName = "nvarchar")]
        public string Position { get; set; }

        [Column("Beschreibung", TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Column("Aktiv", TypeName = "bit")]
        public bool IsContactActive { get; set; }

        [Required]
        [Range(1, 2)]
        [Column("Geschlecht", TypeName = "int")]
        public Gender Gender { get; set; }

        [ForeignKey(nameof(CommunicationModel.LinkId))]
        public List<CommunicationModel> Communications { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public CompanyModel Company { get; set; }

        [ForeignKey(nameof(AddressId))]
        public AddressModel Address { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj as ContactModel) == null)
            {
                return false;
            }

            return Equals(Id, ((ContactModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return LastName;
        }
    }
}