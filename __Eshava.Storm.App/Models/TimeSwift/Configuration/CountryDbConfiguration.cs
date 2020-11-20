using System;
using System.Collections.Generic;
using System.Text;
using Eshava.Storm.MetaData.Builders;
using Eshava.Storm.MetaData.Interfaces;
using TimeSwift.Models.Data.BasicInformation;
using TimeSwift.Models.Data.BasicInformation.Employees;

namespace Eshava.Storm.App.Models.TimeSwift.Configuration
{
	public class CountryDbConfiguration : IEntityTypeConfiguration<CountryModel>
	{
		public void Configure(EntityTypeBuilder<CountryModel> builder)
		{
			builder
				.ToTable("tbl_Countries")
				.Property(p => p.Id).IsKey().ValueGeneratedOnAdd()
				;
		}
	}
}
