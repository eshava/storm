using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class ToolsAndMaterialsItemModel : EquatableObject<ToolsAndMaterialsItemModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("71d73397-1431-456b-a873-5784f8c08f08").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(20)]
		public string InventoryNumber { get; set; }

		[Required]
		[MaxLength(50)]
		public string InventoryName { get; set; }

		[MaxLength(50)]
		public string ManufacturerSerialNumber { get; set; }

		[Required]
		public Guid InventoryTypeId { get; set; }

		[DataType(DataType.Date)]
		public DateTime PurchaseDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ValidTo { get; set; }

		public ToolsAndMaterialsTypeModel Type { get; set; }
	}
}
