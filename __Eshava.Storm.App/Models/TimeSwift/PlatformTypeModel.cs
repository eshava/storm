using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class PlatformTypeModel : EquatableObject<PlatformTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("f1a784e7-d262-4f4c-975a-a1239ad19839").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string PlatformTypeName { get; set; }

		[Required]
		public Guid? PlatformManufacturerId { get; set; }

		[Range(0, Int32.MaxValue)]
		public int PlatformWidthInMillimeter { get; set; }

		[Range(0, Int32.MaxValue)]
		public int PlatformHeightInMillimeter { get; set; }

		[Range(0, Int32.MaxValue)]
		public int PlatformLengthInMillimeter { get; set; }

		public PlatformManufacturerModel Manufacturer { get; set; }
	}
}