namespace TimeSwift.Models.Data.Enums
{
	public enum RightName
	{
		GlobalAdministrator = 1,
		LocalAdministrator = 2,
		MobileApp = 3,
		WebFrontend = 4,

		/* BasicInformation */
		BasicInformationCommon = 1000,
		DocumentType = 1001,
		Country = 1002,
		TimeInterval = 1003,
		WorkActivity = 1004,
		WorkActivityType = 1005,
		WorkActivityUsageCategory = 1006,
		CostType = 1007,
		FinancialInstitution = 1008,
		CostCenter = 1009,
		FuelCard = 1010,
		FuelCardProvider = 1011,
		ToolsAndMaterialsType = 1013,
		ToolsAndMaterialsItem = 1014,

		/* BasicInformation - Vehicles */
		Vehicles = 3000 /* access to all basic information for vehicles */,
		FuelType = 3001,
		VehicleType = 3003,
		SpecialSuperstructure = 3004,
		VehicleCompanyLocation = 3005,
		VehicleModel = 3006,
		LiftgateType = 3007,
		PlatformType = 3008,
		PlatformManufacturer = 3009,
		VehicleManufacturer = 3010,
		MaintenanceContractType = 3011,
		Vehicle = 3012,
		LiftgateManufacturer = 3013,
		SpecialSuperstructureManufacturer = 3014,
		SpecialSuperstructureType = 3015,
		EmissionClass = 3016,
		VehicleOwner = 3017,
		TelematicBox = 3018,
		Tire = 3019,

		/* BasicInformation - Employees */
		Employees = 2000 /* access to all basic information for employees */,
		EmployeeType = 2001,
		SalaryComponent = 2002,
		HealthInsuranceCompany = 2003,
		VehicleLicenceClass = 2004,
		ClothingType = 2005,
		MobilePhone = 2006,
		Employer = 2007,
		WorkLocation = 2008,
		VolumeOfEmployment = 2009,
		Qualification = 2010,
		MobilePhoneProvider = 2011,
		MobilePhoneTariff = 2012,
		EquipmentType = 2013,
		Employee = 2014,
		FundedPensionType = 2015,
		FundedPensionFinancingType = 2016,
		MobilePhoneContract = 2017,
		Nationality = 2018,

		/* BasicInformation - Tourdefinitions */
		TourDefinitions = 4000 /* access to all basic information for tour definitions */,
		TourDefinition = 4001,

		/* BasicInformation - Companies */
		Companies = 5000 /* access to all basic information for tour definitions */,
		Company = 5001,

		/* Application */
		Applications = 6000,
		Plugins = 6001,
		InventoryNumberSettings = 6002,
		FuelCardAssignment = 6003,
		ToolsAndMaterialsAssignment = 6004,
		UserToEmployeeAssignment = 6005,

		/* BaiscInformation - OnBoarding */
		OnBoarding = 7000 /* access to all basic information for onBoardings */
	}
}