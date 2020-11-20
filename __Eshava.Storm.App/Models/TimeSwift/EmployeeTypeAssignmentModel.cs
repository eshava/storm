using System;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeTypeAssignmentModel : IEquatable<EmployeeTypeAssignmentModel>, IN2NIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("62664d9a-3cae-4376-b0bf-5dd78304d1df").GetHashCode();

		public Guid EmployeeId { get; set; }

		public Guid EmployeeTypeId { get; set; }

		public EmployeeTypeModel EmployeeType { get; set; }
		public EmployeeModel Employee { get; set; }

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is EmployeeTypeAssignmentModel assignment))
			{
				return false;
			}

			return assignment.EmployeeId.Equals(EmployeeId) && assignment.EmployeeTypeId.Equals(EmployeeTypeId);
		}

		public bool Equals(EmployeeTypeAssignmentModel assignment)
		{
			return assignment != null && assignment.EmployeeId.Equals(EmployeeId) && assignment.EmployeeTypeId.Equals(EmployeeTypeId);
		}
	}
}