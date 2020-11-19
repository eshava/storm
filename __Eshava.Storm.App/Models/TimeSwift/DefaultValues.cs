using System;

namespace TimeSwift.Models.Data.BasicInformation
{
	public static class DefaultValues
	{
		public static class Onboarding
		{
			public static readonly Guid ExternalUserId = Guid.Parse("ad47dd2d-b326-46aa-80dc-bb5cdf7c5e4e");
		}

		public static class Qualifications
		{
			public static readonly Guid Entry95 = Guid.Parse("c4e3a8fe-8d11-4869-8d9c-699adb95f1e9");
		}

		public static class Countries
		{
			public static Guid GermanyId = Guid.Parse("e94dc4eb-8743-46a9-b595-80d48ede9853");
		}

		public static class InventoryNumbers
		{
			public static readonly Guid ToolsAndMaterials = Guid.Parse("49a484a4-4244-44d8-8d28-a1ac4adc1907");
			public static readonly Guid Vehicle = Guid.Parse("b42914a4-ed75-4e59-9061-79ed574aa091");
			public static readonly Guid VehiclePlatform = Guid.Parse("cbef0496-b07b-4d66-af65-4d08fcd79130");
			public static readonly Guid SpecialSuperstructure = Guid.Parse("a223a875-6c06-4c36-be28-7d3c174f87f5");

		}

		public static class EmissionClasses
		{
			public static readonly Guid Euro1 = Guid.Parse("b0cf48eb-038c-47cd-b0cb-a93c5573e9f3");
			public static readonly Guid Euro2 = Guid.Parse("a2fd9c6d-dbb6-4a48-aa15-f0ddb72c438e");
			public static readonly Guid Euro3 = Guid.Parse("2219851e-8f1c-404f-a0ed-2b55f8dd21a5");
			public static readonly Guid Euro4 = Guid.Parse("216f006f-d6f0-47da-986d-8cc2787c28cd");
			public static readonly Guid Euro5 = Guid.Parse("425787b6-d565-4f08-b4db-7d39d36d9757");
			public static readonly Guid Euro6 = Guid.Parse("22b06d4f-a4a5-46e5-aa4f-75d936eb19ce");
			public static readonly Guid EuroI = Guid.Parse("8f1f7163-b56b-4cb1-aaa3-1bcb17f53704");
			public static readonly Guid EuroII = Guid.Parse("f553f58e-894c-4711-8cd0-e82bd3f197d4");
			public static readonly Guid EuroIII = Guid.Parse("b6bc1b3e-95d2-4789-8d97-8825eab1bf05");
			public static readonly Guid EuroIV = Guid.Parse("0f842f7f-6aa6-406d-9bc4-f6affbee31e7");
			public static readonly Guid EuroV = Guid.Parse("eb2821fd-656c-4b45-be8c-17add7adfa53");
			public static readonly Guid EuroVI = Guid.Parse("3c75230d-68a2-4455-a60a-07ce17686528");
		}

		public static readonly Guid DefaultVehicleOwnerId = Guid.Parse("3fc8d992-5623-4bcb-9957-5a14f6f33f38");
	}
}