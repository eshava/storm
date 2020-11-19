using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshava.RP365.Models.Data.Base.BusinessPartnerManagement
{
	[Table("tbl_Adressverwaltung")]
    public class CompanyModel 
    {
        private static readonly int _hashCode = Guid.Parse("cf6662db-7e7c-4c9f-a7c8-618ff50aaa23").GetHashCode();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("UnternehmenID", TypeName = "uniqueidentifier")]
        public Guid? Id { get; set; }

        [Column("Aktiv", TypeName = "bit")]
        public bool IsActive { get; set; }

        [MaxLength(50)]
        [Column("AdressNr", TypeName = "nvarchar")]
        public string AddressNumber { get; set; }
        
        [ForeignKey(nameof(AddressModel.CompanyId))]
        public List<AddressModel> Addresses { get; set; }

        [ForeignKey(nameof(ContactModel.CompanyId))]
        public List<ContactModel> Contacts { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj as CompanyModel) == null)
            {
                return false;
            }

            return Equals(Id, ((CompanyModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return AddressNumber;
        }
    }
}