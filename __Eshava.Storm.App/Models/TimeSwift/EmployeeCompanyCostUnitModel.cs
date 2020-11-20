using System;
using TimeSwift.Models.Data.BasicInformation.Companies;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeCompanyCostUnitModel : IEquatable<EmployeeCompanyCostUnitModel>, IN2NIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("4782fb37-e0a2-47ad-ab49-7b9b19e0190b").GetHashCode();

		public Guid EmployeeId { get; set; }

		public Guid CompanyCostUnitId { get; set; }

		public CompanyCostUnitModel CompanyCostUnit { get; set; }
		public EmployeeModel Employee { get; set; }

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is EmployeeCompanyCostUnitModel assignment))
			{
				return false;
			}

			return assignment.EmployeeId.Equals(EmployeeId) && assignment.CompanyCostUnitId.Equals(CompanyCostUnitId);
		}

		public bool Equals(EmployeeCompanyCostUnitModel assignment)
		{
			return assignment != null && assignment.EmployeeId.Equals(EmployeeId) && assignment.CompanyCostUnitId.Equals(CompanyCostUnitId);
		}
	}
}