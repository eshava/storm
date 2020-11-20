using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class VolumeOfEmploymentModel : EquatableObject<VolumeOfEmploymentModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("950fc6f4-7603-4fc1-ad7a-b1fb3c7cb505").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[Range(0, Int32.MaxValue)]
		public int DefaultDailyWorkTimeInHours { get; set; }
	}
}