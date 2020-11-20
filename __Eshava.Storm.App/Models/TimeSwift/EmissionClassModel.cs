using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class EmissionClassModel : EquatableObject<EmissionClassModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("97f400ef-56f2-4ccc-b9f6-8a7b48b0a920").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(10)]
		public string EmissionClassName { get; set; }
	}
}