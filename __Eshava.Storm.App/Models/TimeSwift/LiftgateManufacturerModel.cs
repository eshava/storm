using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class LiftgateManufacturerModel : EquatableObject<LiftgateManufacturerModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("d9a232d8-472c-4d47-a923-347b374fd656").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Manufacturer { get; set; }

		public List<LiftgateTypeModel> LiftgateTypes { get; set; }
	}
}