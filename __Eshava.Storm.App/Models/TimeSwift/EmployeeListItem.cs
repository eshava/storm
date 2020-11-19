using System;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeListItem
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? ContractEnd { get; set; }
	}
}