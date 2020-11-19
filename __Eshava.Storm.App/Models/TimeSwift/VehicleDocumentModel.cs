using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleDocumentModel : EquatableObject<VehicleDocumentModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("15eb2e53-2944-458e-9d79-2b59a139fe94").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		public Guid VehicleId { get; set; }
		
		[Required]
		[MaxLength(50)]
		public string FileName { get; set; }

		[Required]
		public Guid? DocumentTypeId { get; set; }

		public DocumentTypeModel DocumentType { get; set; }
	}
}
