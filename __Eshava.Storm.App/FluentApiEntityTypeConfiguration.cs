using System;
using System.Collections.Generic;
using System.Text;
using Eshava.Storm.App.Models;
using Eshava.Storm.MetaData.Builders;
using Eshava.Storm.MetaData.Interfaces;

namespace Eshava.Storm.App
{
	public class FluentApiEntityTypeConfiguration : IEntityTypeConfiguration<FluentApiEntity>
	{
		public void Configure(EntityTypeBuilder<FluentApiEntity> builder)
		{
			//builder
			//	.ToTable("FakeNews")
			//	.Property(p => p.PrimiaryKey).IsKey().ValueGeneratedOnAdd()
			//	;

			//builder.OwnsOne(p => p.OwnsOneProperty, ba =>
			//{
			//	ba.Property(p => p.AutoColumn).ValueGeneratedOnAddOrUpdate();
			//});

			builder.HasKey(p => new { p.PrimiaryKey, p.Name });
			builder.Property(p => p.Name).HasColumnName("Naaaaaaaaaaaaaaaaaaame");

		}
	}
}
