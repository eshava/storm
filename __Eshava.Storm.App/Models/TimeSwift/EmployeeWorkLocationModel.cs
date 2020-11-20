using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	[Table("tbl_Employee_WorkLocations")]
	public class EmployeeWorkLocationModel : IEquatable<EmployeeWorkLocationModel>, IN2NIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("c450f7f2-697b-4ae9-b4f8-736bf3b93b7f").GetHashCode();

		[Key]
		public Guid EmployeeId { get; set; }

		[Key]
		public Guid WorkLocationId { get; set; }

		public WorkLocationModel WorkLocation { get; set; }
		public EmployeeModel Employee { get; set; }

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is EmployeeWorkLocationModel assignment))
			{
				return false;
			}

			return assignment.EmployeeId.Equals(EmployeeId) && assignment.WorkLocationId.Equals(WorkLocationId);
		}

		public bool Equals(EmployeeWorkLocationModel assignment)
		{
			return assignment != null && assignment.EmployeeId.Equals(EmployeeId) && assignment.WorkLocationId.Equals(WorkLocationId);
		}
	}
}