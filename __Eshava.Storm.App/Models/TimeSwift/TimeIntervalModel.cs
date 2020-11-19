using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Enums;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation
{
	public class TimeIntervalModel : EquatableObject<TimeIntervalModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("85ef640a-930a-4393-87cf-9f585377192a").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string IntervalName { get; set; }

		[Range(0, Int32.MaxValue)]
		public int Interval { get; set; }

		public TimeIntervalType IntervalType { get; set; }
	}
}