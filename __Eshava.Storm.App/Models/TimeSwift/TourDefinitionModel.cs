using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.BasicInformation.Companies;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Enums;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Tours
{
	public class TourDefinitionModel : EquatableObject<TourDefinitionModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("eb863d96-59ff-4fc7-a6db-ce5e26080147").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TourDefinitionName { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[Required]
		public Guid PriceKeyFactorId { get; set; }

		public Guid? CompanyCostUnitId { get; set; }

		[DataType(DataType.MultilineText)]
		public string Remarks { get; set; }

		[Required]
		[Range(0, 127)]
		public Weekdays Weekdays { get; set; }

		[DataType(DataType.Time)]
		public TimeSpan? StartOfWork { get; set; }

		public bool IsAccountingRelevant { get; set; }

		public bool IsLoadSecuringPhotoRequired { get; set; }

		public bool IsWorkingTimeRequired { get; set; }

		public PriceKeyFactorModel PriceKeyFactor { get; set; }
		public CompanyCostUnitModel CompanyCostUnit { get; set; }

		/* Open questions */

		/// <summary>
		/// Purpose of the property?
		/// </summary>
		[DataType(DataType.Date)]
		public DateTime? ValidFrom { get; set; }

		/// <summary>
		/// Purpose of the property?
		/// </summary>
		[DataType(DataType.Date)]
		public DateTime? ValidTo { get; set; }
	}
}