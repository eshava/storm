using System;
using TimeSwift.Models.Data.BasicInformation.Companies;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class VehicleCompanyLocationModel : IEquatable<VehicleCompanyLocationModel>, IN2NIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("6320480e-a4a9-473c-a581-ad07b2cdf51e").GetHashCode();

		public Guid VehicleId { get; set; }

		public Guid CompanyLocationId { get; set; }

		public CompanyLocationModel CompanyLocation { get; set; }
		public VehicleModel Vehicle { get; set; }

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is VehicleCompanyLocationModel assignment))
			{
				return false;
			}

			return assignment.VehicleId.Equals(VehicleId) && assignment.CompanyLocationId.Equals(CompanyLocationId);
		}

		public bool Equals(VehicleCompanyLocationModel assignment)
		{
			return assignment != null && assignment.VehicleId.Equals(VehicleId) && assignment.CompanyLocationId.Equals(CompanyLocationId);
		}
	}
}