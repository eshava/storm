using System;
using System.ComponentModel.DataAnnotations;

using TimeSwift.Models.Data.BasicInformation.Companies;
using TimeSwift.Models.Data.BasicInformation.Employees;
using TimeSwift.Models.Data.BasicInformation.Vehicles;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Tours
{
	public class PriceStructureModel : EquatableObject<PriceStructureModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("b1ae8806-71fb-4ef6-8261-f6312bb75170").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		[Required]
		
		public Guid CompanyCostUnitId { get; set; }

		[Required]
		
		public Guid PriceKeyFactorId { get; set; }

		
		public Guid? VehicleTypeId { get; set; }

		
		public Guid? EmployeeTypeId { get; set; }

		
		public Guid? TourDefinitionId { get; set; }

		
		public Guid? WorkActivityId { get; set; }

		[Required]
		
		[Range(typeof(decimal), "0", "1000000")]
		public decimal Price { get; set; }

		public bool IsActive { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ValidUntil { get; set; }

		[DataType(DataType.MultilineText)]
		public string Remarks { get; set; }

		public CompanyCostUnitModel CompanyCostUnit { get; set; }
		public PriceKeyFactorModel PriceKeyFactor { get; set; }
		public VehicleTypeModel VehicleType { get; set; }
		public EmployeeTypeModel EmployeeType { get; set; }
		public TourDefinitionModel TourDefinition { get; set; }
		public WorkActivityModel WorkActivity { get; set; }
	}
}