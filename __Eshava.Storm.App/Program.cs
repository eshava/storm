using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Eshava.Storm.Extensions;
using Eshava.Storm.MetaData;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Eshava.Storm.App
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");


			await TimeSwiftAsync();
			await RP365Async();






		}

		private static async Task RP365Async()
		{
			//			var connectionString = "Data Source=03-K64;Initial Catalog=rp365-0479b800-5b8d-428d-af55-2d9aaa1354bb;Integrated Security=True;";
			//			var connection = new SqlConnection(connectionString);

			//			var invoicesQuery = $@"
			//SELECT
			//	 i.*
			//	,ip.*
			//	,qu.*
			//	,a.*
			//	,vat.*
			//	,cur.*
			//	,cou.*
			//	,addr.*
			//FROM tbl_Lieferantendokumente i
			//LEFT OUTER JOIN tbl_Waehrungen cur on cur.WaehrungID = i.WaehrungId
			//LEFT OUTER JOIN tbl_Laender cou on cou.LandID = i.A_LandId
			//LEFT OUTER JOIN tbl_Adressverwaltung_Anschriften addr on addr.AnschriftID = i.AnschriftId
			//LEFT OUTER JOIN tbl_LieferantendokumentePositionen ip on ip.LieferantendokumentId = i.LieferantendokumentId
			//LEFT OUTER JOIN tbl_Mengeneinheiten qu on qu.MengeneinheitID = ip.MengeneinheitId
			//LEFT OUTER JOIN tbl_Umsatzsteuersaetze vat on vat.UmsatzsteuersatzID = ip.UmsatzsteuersatzId
			//LEFT OUTER JOIN tbl_Artikel a on a.ArtikelID = ip.ArtikelId
			//WHERE i.LieferantendokumentId = 'E713BBE6-CE62-4DE8-B994-1651464AA90A'
			//ORDER BY i.LieferantendokumentId";

			//			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			//			sw.Start();

			//			for (int i = 0; i < 1379; i++)
			//			{


			//				var invoices = await connection.QueryAsync(
			//					invoicesQuery,
			//					mapper =>
			//					{
			//						var i = mapper.Map<RP365.Models.Data.PurchaseManagement.SupplierDocumentModel>("i");
			//						var p = mapper.Map<RP365.Models.Data.PurchaseManagement.SupplierDocumentPositionModel>("p");
			//						var qu = mapper.Map<RP365.Models.Data.Administration.BasicInformation.QuantityUnitModel>("qu");
			//						var vat = mapper.Map<RP365.Models.Data.Administration.BasicInformation.VATRateModel>("vat");
			//						var a = mapper.Map<RP365.Models.Data.Base.ArticleManagement.ArticleModel>("a");
			//						var cur = mapper.Map<RP365.Models.Data.Administration.BasicInformation.CurrencyModel>("cur");
			//						var cou = mapper.Map<RP365.Models.Data.Administration.BasicInformation.CountryModel>("cou");
			//						var add = mapper.Map<RP365.Models.Data.Base.BusinessPartnerManagement.AddressModel>("addr");

			//						i.AddressCountry = cou;
			//						i.Currency = cur;
			//						i.Address = add;

			//						p.QuantityUnit = qu;
			//						p.VATRate = vat;
			//						p.Article = a;

			//						i.Positions = new List<RP365.Models.Data.PurchaseManagement.SupplierDocumentPositionModel> { p };

			//						return i;
			//					});
			//			}
			//			sw.Stop();

			//			if (true)
			//			{

			//			}
		}


		private class VehicleJoin
		{
			public Guid Id { get; set; }
			public string LicensePlate { get; set; }
			public string ManufacturerCompany { get; set; }
			public Guid ManufacturerId { get; set; }
			public Dictionary<string, string> Json { get; set; }
		}

		private static TransactionScope CreateTransactionScope(TransactionScopeAsyncFlowOption option = TransactionScopeAsyncFlowOption.Suppress)
		{
			return new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = new TimeSpan(0, 30, 0) }, option);
		}

		private static async Task TimeSwiftAsync()
		{
			var ddd = new Dictionary<string, string>
			{
				{ "AAA", "BBB" }
			};

			//var dddJson = JsonConvert.SerializeObject(ddd);

			//new DicTypeHandler().AddTypeHandler();

			var connectionString = "Data Source=03-K64;Initial Catalog=TimeSwiftOneEFCore;Integrated Security=True;";


			try
			{
				using (var transaction = CreateTransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					using (var connectionTrans = new SqlConnection(connectionString))
					{

						var country = new TimeSwift.Models.Data.BasicInformation.CountryModel
						{
							Id = Guid.Parse("b3bd755f-8dbc-44f6-81cc-9a42bb38904e"),
							Country = "Spanien",
							CountryCode = "ES"
						};

						var result = await connectionTrans.InsertAsync<TimeSwift.Models.Data.BasicInformation.CountryModel, Guid>(country);

						var countryDb = await connectionTrans.QueryEntityAsync<TimeSwift.Models.Data.BasicInformation.CountryModel>(country.Id.Value);

						var resultDelete = await connectionTrans.DeleteAsync<TimeSwift.Models.Data.BasicInformation.CountryModel>(country);
						if (true)
						{

						}

						
					}
				}
			}
			catch (Exception ex)
			{

				throw;
			}
			
			
			
			
			
			var connection = new SqlConnection(connectionString);


			//var vehicle = await connection.QueryEntityAsync<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleModel>(Guid.Parse("26B5DF44-4D22-41C2-9158-5AE52681DFDB"));

			//var ewl = await connection.QueryEntityAsync<TimeSwift.Models.Data.BasicInformation.Employees.EmployeeWorkLocationModel>(new
			//{
			//	EmployeeId = Guid.Parse("b1a758f1-a873-42ea-a234-8ad6a36e7844"),
			//	WorkLocationId = Guid.Parse("a4f84781-90e2-49e7-aaf0-4b63f087baa9")
			//});
			//;

			//var vehicle = await connection.QueryFirstOrDefaultAsync<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleModel>(
			//	$"SELECT v.* FROM {TypeAnalyzer.GetTableName<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleModel>()} v WHERE Id = @Id",
			//	new { Id = Guid.Parse("45C01747-B4CE-4648-AD91-AE391756B7C7") });




			//			var joinQuery = $@"
			//SELECT
			//	 v.Id
			//	,v.RegistrationNumber AS LicensePlate
			//	,m.Manufacturer AS ManufacturerCompany
			//	,m.Id AS ManufacturerId
			//	,v.Remarks AS Json
			//FROM tbl_Vehicles v
			//JOIN tbl_Vehicle_Manufacturers m on m.Id = v.Basics_ManufacturerId
			//WHERE
			//	v.Id = @Id
			//";

			//			var parameter = new { Id = Guid.Parse("26B5DF44-4D22-41C2-9158-5AE52681DFDB") };


			//			var vehicleJoin = await connection.QueryAsync(
			//				joinQuery, 
			//				mapper =>
			//				{
			//					var v1 = mapper.Map<VehicleJoin>("v,m");
			//					var v2 = mapper.Map<VehicleJoin>("v");
			//					var v3 = mapper.Map<VehicleJoin>("m");
			//					var v4 = mapper.Map<VehicleJoin>();

			//					return default(VehicleJoin);
			//				}, 
			//				parameter);


			//			var vehicleCount = await connection.ExecuteScalarAsync<int>("SELECT Count(Id) FROM tbl_Vehicles");

			//			var vehicleQuery = @"
			//SELECT * FROM tbl_Vehicles
			//WHERE Basics_ManufacturerId = @Manufacturer";
			//			var vehiclesWithoutSubs = await connection.QueryAsync<VehicleModel>(vehicleQuery, new { Manufacturer = Guid.Parse("CE93F269-3E3A-471D-B91D-4377A971D319") });


			//var vehicleQueryWithSubs = @"
			//SELECT 
			//	 v.*
			//	,vm.*
			//	,vt.*
			//FROM 
			//	tbl_Vehicles v
			//LEFT OUTER JOIN tbl_Vehicle_Manufacturers vm ON vm.Id = v.Basics_ManufacturerId
			//LEFT OUTER JOIN tbl_Vehicle_Tanks vt on vt.VehicleId = v.Id
			//WHERE 
			//	v.Basics_ManufacturerId IN @Manufacturer";


			//var vehicles = await connection.QueryAsync<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleModel>(
			//	vehicleQueryWithSubs,
			//	mapper =>
			//	{
			//		var v = mapper.Map<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleModel>("v");
			//		var vm = mapper.Map<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleManufacturerModel>("vm");
			//		var vt = mapper.Map<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleTankModel>("vt");
			//		v.Basics.Manufacturer = vm;
			//		if (vm != default)
			//		{
			//			v.VehicleTanks = new System.Collections.Generic.List<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleTankModel>() { vt };
			//		}

			//		return v;
			//	},
			//	new { Manufacturer = new List<Guid> { Guid.Parse("CE93F269-3E3A-471D-B91D-4377A971D319"), Guid.Parse("21266F77-1784-4841-9C19-4DAE6F1380EF") } });


			//			var manufacturerCountQuery = @"
			//SELECT 
			//	 v.Basics_ManufacturerId AS ManufacturerId
			//	,Count(v.Id) AS NumberOfUsages
			//FROM tbl_Vehicles v
			//GROUP BY v.Basics_ManufacturerId
			//";

			//			var manufacturerCounts = await connection.QueryAsync<Dummy>(manufacturerCountQuery,
			//				mapper =>
			//				{
			//					var v = mapper.Map<Dummy>("vm");

			//					return v;
			//				});


			/*
			 	--FROM tbl_Employees e
			--LEFT OUTER JOIN tbl_Countries c1 ON c1.Id = e.CountryId
			--LEFT OUTER JOIN tbl_Countries c2 ON c2.Id = e.DriverCardCountryId

			FROM {TypeAnalyzer.GetTableName<TimeSwift.Models.Data.BasicInformation.Employees.EmployeeModel>()} e
			LEFT OUTER JOIN {TypeAnalyzer.GetTableName<TimeSwift.Models.Data.BasicInformation.CountryModel>()} c1 ON c1.Id = e.CountryId
			LEFT OUTER JOIN {TypeAnalyzer.GetTableName<TimeSwift.Models.Data.BasicInformation.CountryModel>()} c2 ON c2.Id = e.DriverCardCountryId
			 
			 */

			Eshava.Storm.MetaData.TypeAnalyzer.AddType(new Models.TimeSwift.Configuration.EmployeeDbConfiguration());
			//Eshava.Storm.MetaData.TypeAnalyzer.AddType(new Models.TimeSwift.Configuration.CountryDbConfiguration());

			var employeeQuery = $@"
			SELECT
				 e.*
				,c1.*
				,c2.*
			 	FROM dbo.tbl_Employees e
			LEFT OUTER JOIN dbo.tbl_Countries c1 ON c1.Id = e.CountryId
			LEFT OUTER JOIN dbo.tbl_Countries c2 ON c2.Id = e.DriverCardCountryId
			
			";
			var employees = await connection.QueryAsync<TimeSwift.Models.Data.BasicInformation.Employees.EmployeeModel>(
				employeeQuery,
				mapper =>
				{
					var v = mapper.Map<TimeSwift.Models.Data.BasicInformation.Employees.EmployeeModel>("e");
					var vm = mapper.Map<TimeSwift.Models.Data.BasicInformation.CountryModel>("c1");
					var vt = mapper.Map<TimeSwift.Models.Data.BasicInformation.CountryModel>("c2");

					v.Country = vm;
					v.DriverCardCountry = vt;

					return v;
				});


			//var country = new CountryModel
			//{
			//	Id = Guid.Parse("b3bd755f-8dbc-44f6-81cc-9a42bb38904e"),
			//	Country = "Spanien",
			//	CountryCode = "ES"
			//};

			//var result = await connection.InsertAsync<CountryModel, Guid>(country);

			//var resultUpdate = await connection.UpdatePartialAsync<CountryModel>(new
			//{
			//	Id = Guid.Parse("b3bd755f-8dbc-44f6-81cc-9a42bb38904e"),
			//	Country = "Belgien"
			//});

			//country.Country = "Belgien";
			//country.CountryCode = "BE";

			//var resultUpdate = await connection.UpdateAsync(country);

			//var resultDelete = await connection.DeleteAsync(country);

			//var partialVehicle = new
			//{
			//	Id = Guid.Parse("5EED403D-0B0C-4993-893B-08003F9970E3"),
			//	Basics = new
			//	{
			//		EnvironmentalBadge = TimeSwift.Models.Data.Enums.EnvironmentalBadge.Yellow
			//	}
			//};

			//var resultUpdate = await connection.UpdatePartialAsync<TimeSwift.Models.Data.BasicInformation.Vehicles.VehicleModel>(partialVehicle);

			//var fakeCountries = new List<TimeSwift.Models.Data.BasicInformation.CountryModel>();
			//fakeCountries.Add(new TimeSwift.Models.Data.BasicInformation.CountryModel
			//{
			//	Id = Guid.NewGuid(),
			//	Country = "AAAAAA",
			//	CountryCode = "AA"
			//});
			//fakeCountries.Add(new TimeSwift.Models.Data.BasicInformation.CountryModel
			//{
			//	Id = Guid.NewGuid(),
			//	Country = "BBBBBB",
			//	CountryCode = "BB"
			//});
			//fakeCountries.Add(new TimeSwift.Models.Data.BasicInformation.CountryModel
			//{
			//	Id = Guid.NewGuid(),
			//	Country = "CCCCCC",
			//	CountryCode = "CC"
			//});
			//fakeCountries.Add(new TimeSwift.Models.Data.BasicInformation.CountryModel
			//{
			//	Id = Guid.NewGuid(),
			//	Country = "DDDDDDD",
			//	CountryCode = "DD"
			//});
			//fakeCountries.Add(new TimeSwift.Models.Data.BasicInformation.CountryModel
			//{
			//	Id = Guid.NewGuid(),
			//	Country = "EEEEEEE",
			//	CountryCode = "EE"
			//});
			//fakeCountries.Add(new TimeSwift.Models.Data.BasicInformation.CountryModel
			//{
			//	Id = Guid.NewGuid(),
			//	Country = "FFFFFF",
			//	CountryCode = "FF"
			//});

			//try
			//{
			//	await connection.OpenAsync();
			//	await connection.BulkInsertAsync<TimeSwift.Models.Data.BasicInformation.CountryModel>(fakeCountries);
			//	await connection.CloseAsync();
			//}
			//catch (Exception ex)
			//{

			//	throw;
			//}

			//var country = new TimeSwift.Models.Data.BasicInformation.CountryModel
			//{
			//	Id = Guid.Parse("b3bd755f-8dbc-44f6-81cc-9a42bb38904e"),
			//	Country = "Spanien",
			//	CountryCode = "ES"
			//};

			//var result = await connection.InsertAsync<TimeSwift.Models.Data.BasicInformation.CountryModel, Guid>(country);

			//country.Country = "Fake";
			//country.CountryCode = "FA";

			//System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			//sw.Start();
			//for (int i = 0; i < 10000; i++)
			//{
			//	var result2 = await connection.UpdateAsync(country);
			//}
			//sw.Stop();


			//var resultDelete = await connection.DeleteAsync(country);

			//MetaData.TypeAnalyzer.AddType(new FluentApiEntityTypeConfiguration());

			//Eshava.Storm.MetaData.TypeAnalyzer.AddType(new FluentApiEntityTypeConfiguration());
			if (true)
			{

			}
		}

		private class Dummy
		{
			public Guid? ManufacturerId { get; set; }
			public int NumberOfUsages { get; set; }
		}
	}
}
