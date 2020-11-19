using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployerModel : EquatableObject<EmployerModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("1b77e0bd-e38c-4460-82f9-592c6b2278c5").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string EmployerName { get; set; }
	}
}