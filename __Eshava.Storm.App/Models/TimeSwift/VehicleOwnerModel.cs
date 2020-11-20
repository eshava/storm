using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleOwnerModel : EquatableObject<VehicleOwnerModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("0f94b7ea-df5e-4d25-949c-28a971db52c8").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[MaxLength(50)]
		public string Owner { get; set; }
	}
}