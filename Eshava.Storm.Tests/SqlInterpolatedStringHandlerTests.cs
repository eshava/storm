using System;
using System.Linq;
using Eshava.Storm.Handler;
using Eshava.Storm.MetaData;
using Eshava.Storm.MetaData.Builders;
using Eshava.Storm.MetaData.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eshava.Storm.Tests
{
	[TestClass]
	public sealed class SqlInterpolatedStringHandlerTests
	{
		[AssemblyInitialize]
		public static void AssemblyInitialize(TestContext context)
		{
			TypeAnalyzer.AddType(new AlphaDbConfiguation());
		}

		[TestMethod]
		public void BasicTest()
		{
			var searchString = "A search string";

			SqlInterpolatedStringHandler sqlInterpolatedStringHandler = $"""
				SELECT
					alpha.*
				FROM
					{TypeAnalyzer.GetTableName<Alpha>()} alpha
				WHERE
					alpha.{nameof(Alpha.Description)} = @{searchString}
				ORDER BY
					{nameof(Alpha.Id)} desc
				""";

			var query = sqlInterpolatedStringHandler.ToStringAndClear();
			var parameters = sqlInterpolatedStringHandler.Parameters;

			query.Should().Contain("[alphaScheme].[Alphas] alpha");

			parameters.Should().HaveCount(1);
			parameters.First().Value.Should().Be(searchString);
		}

		private class Alpha
		{
			public int Id { get; set; }
			public bool HasSomething { get; set; }
			public string Description { get; set; }
			public DateTime ChangedAtUtc { get; set; }
		}

		private class AlphaDbConfiguation : IEntityTypeConfiguration<Alpha>
		{
			public void Configure(EntityTypeBuilder<Alpha> builder)
			{
				builder.HasKey(x => x.Id);
				builder.ToTable("Alphas", "alphaScheme");
			}
		}
	}
}
