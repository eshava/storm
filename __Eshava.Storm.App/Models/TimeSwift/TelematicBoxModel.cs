using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class TelematicBoxModel : EquatableObject<TelematicBoxModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("456de647-7c07-4484-a53b-93701687d002").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string BoxName { get; set; }
	}
}