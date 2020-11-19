using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class VehicleLicenceClassModel : EquatableObject<VehicleLicenceClassModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("8c2d598f-a617-48a2-a6a1-7626e8a2fc14").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string LicenceClassName { get; set; }
	}
}