using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class WorkLocationModel : EquatableObject<WorkLocationModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("5cbbe194-c0c6-4047-9ab1-75f458af5f1a").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string WorkLocation { get; set; }
	}
}