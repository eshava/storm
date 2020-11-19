using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Enums;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class FuelTypeModel : EquatableObject<FuelTypeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("e201dea6-0d3c-45d7-8dd7-eecbba985db8").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TypeName { get; set; }

		[Required]
		public FuelTypeMeasurementUnit MeasurementUnit { get; set; }
	}
}