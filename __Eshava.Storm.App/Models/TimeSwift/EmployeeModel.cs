using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;
using TimeSwift.Models.Data.BasicInformation.Companies;
using TimeSwift.Models.Data.BasicInformation.Tours;
using TimeSwift.Models.Data.BasicInformation.Vehicles;
using TimeSwift.Models.Data.Common;
using TimeSwift.Models.Data.Enums;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.BasicInformation.Employees
{
	public class EmployeeModel : EditorDataModel<EmployeeModel>, IIdentifier
	{
		private static readonly int _hashCode = Guid.Parse("652c901e-6834-413e-8741-f4b27bfdf3be").GetHashCode();
		protected override int HashCode => _hashCode;

		public override Guid? Id { get; set; }

		public bool IsExternalEmployee { get; set; }

		/// <summary>
		/// Relationship column for application user
		/// </summary>
		[MaxLength(100)]
		public string UserName { get; set; }

		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(50)]
		public string Street { get; set; }

		[Required]
		[MaxLength(50)]
		public string ZIPCode { get; set; }

		[Required]
		[MaxLength(50)]
		public string Place { get; set; }

		[Required]
		
		public Guid CountryId { get; set; }

		
		public Guid? MobilePhoneContractId { get; set; }

		/// <summary>
		/// Alternative field if no mobile phone tariff has been selected
		/// </summary>
		[MaxLength(20)]
		public string MobilePhoneNumber { get; set; }

		[MaxLength(255)]
		public string MailAddress { get; set; }

		[MaxLength(20)]
		public string PrivateMobilePhoneNumber { get; set; }

		[MaxLength(20)]
		public string PrivatePhoneNumber { get; set; }

		[MaxLength(255)]
		public string PrivateMailAddress { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[MaxLength(50)]
		public string PlaceOfBirth { get; set; }

		[MaxLength(50)]
		public string BirthName { get; set; }

		[MaxLength(100)]
		public string Bank { get; set; }

		[MaxLength(30)]
		public string IBAN { get; set; }

		[MinLength(8)]
		[MaxLength(11)]
		public string BIC { get; set; }

		
		public Guid? HealthInsuranceCompanyId { get; set; }

		
		public Guid? MainCompanyCostUnitId { get; set; }

		/// <summary>
		/// ToDo: Change column name
		/// In vehicleModel this column has the name: StandardTourDefinitionId
		/// </summary>
		
		public Guid? DefaultTourDefinitionId { get; set; }

		/// <summary>
		/// ToDo: Change column name to StandardVehicleId
		/// </summary>
		
		public Guid? DefaultVehicleId { get; set; }

		[Required]
		
		public Guid EmployerId { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime StartOfEmploymentContract { get; set; }

		[DataType(DataType.Date)]
		public DateTime? EndOfEmploymentContract { get; set; }

		[DataType(DataType.Date)]
		public DateTime? EndOfProbationaryPeriod { get; set; }

		[DataType(DataType.Date)]
		public DateTime? RetirementDate { get; set; }

		[Required]
		
		public Guid VolumeOfEmploymentId { get; set; }

		[Required]
		[Range(0, Int32.MaxValue)]
		public int DailyWorkTimeInHours { get; set; }

		[Range(0, 100)]
		public int? DegreeOfDisability { get; set; }

		
		public Guid? CompanyMobilePhoneId { get; set; }

		/// <summary>
		/// E.g. TomTom WebFleet user id
		/// </summary>
		[MaxLength(50)]
		public string ExternalId { get; set; }
		[MaxLength(16)]
		public string DriverCardNumber { get; set; }

		public Guid? DriverCardCountryId { get; set; }

		public CountryModel DriverCardCountry { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DriverCardValidUntil { get; set; }

		public CountryModel Country { get; set; }
		public MobilePhoneContractModel MobilePhoneContract { get; set; }
		public HealthInsuranceCompanyModel HealthInsuranceCompany { get; set; }

		[Required]
		public List<EmployeeTypeAssignmentModel> EmployeeTypes { get; set; }

		public CompanyCostUnitModel MainCompanyCostUnit { get; set; }
		public TourDefinitionModel DefaultTourDefinition { get; set; }
		public VehicleModel DefaultVehicle { get; set; }
		public List<EmployeeWorkLocationModel> WorkLocations { get; set; }
		public EmployerModel Employer { get; set; }
		public VolumeOfEmploymentModel VolumeOfEmployment { get; set; }
		public List<EmployeeSalaryComponentModel> SalaryComponents { get; set; }
		public List<EmployeeCompanyCostUnitModel> CompanyCostUnits { get; set; }
		public List<EmployerFundedPensionModel> EmployerFundedPensions { get; set; }
		public List<EmployeePersonalEquipmentModel> PersonalEquipments { get; set; }
		public List<EmployeeQualificationModel> Qualifications { get; set; }
		public List<EmployeeVehicleLicenceClassModel> VehicleLicenceClasses { get; set; }
		public List<EmployeeHolidayEntitlementModel> HolidayEntitlements { get; set; }
		public List<EmployeeDocumentModel> Documents { get; set; }
		public MobilePhoneModel CompanyMobilePhone { get; set; }

		/* Open questions */

		/// <summary>
		/// Defined as automatic field -> where data origin?
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string EmployeeNumber { get; set; }

		/// <summary>
		/// Defined as automatic field -> where data origin?
		/// </summary>
		[Required]
		[MaxLength(50)]
		public string InternalId { get; set; }

		[MaxLength(300)]
		public string AdditionalQualification { get; set; }

		[Required]
		public Disability HasDisability { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DisabilityStart { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DisabilityEnd { get; set; }

		// ToDo: create document upload (multiple) property
	}
}