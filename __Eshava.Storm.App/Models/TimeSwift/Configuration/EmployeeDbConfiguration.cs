using System;
using System.Collections.Generic;
using System.Text;
using Eshava.Storm.MetaData.Builders;
using Eshava.Storm.MetaData.Interfaces;
using TimeSwift.Models.Data.BasicInformation.Employees;

namespace Eshava.Storm.App.Models.TimeSwift.Configuration
{
	public class EmployeeDbConfiguration : IEntityTypeConfiguration<EmployeeModel>
	{
		public void Configure(EntityTypeBuilder<EmployeeModel> builder)
		{
			builder
				.ToTable("tbl_Employees")
				.Property(p => p.Id).IsKey().ValueGeneratedOnAdd()
				;
		}
	}
}
