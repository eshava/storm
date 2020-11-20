using System;
using Eshava.Storm.Linq.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Eshava.Storm.Linq.Tests.Extensions
{
	[TestClass, TestCategory("Extensions")]
	public class StringExtensionsTest
	{
		[TestInitialize]
		public void Setup()
		{

		}

		[TestMethod]
		public void AppendSkipAndTakeTest()
		{
			// Arrange
			var skip = 50;
			var take = 25;
			var query = "SELECT * FROM Alpha";

			// Act
			var result = query.AppendSkipAndTake(skip, take);

			// Assert
			result.Should().Be($"{query}{Environment.NewLine}OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY");
		}

		[TestMethod]
		public void AppendSkipAndTakeNoTakeTest()
		{
			// Arrange
			var skip = 50;
			var take = 0;
			var query = "SELECT * FROM Alpha";

			// Act
			var result = query.AppendSkipAndTake(skip, take);

			// Assert
			result.Should().Be(query);
		}

		[TestMethod]
		public void AppendSkipAndTakeNegativeSkipTest()
		{
			// Arrange
			var skip = -50;
			var take = 25;
			var query = "SELECT * FROM Alpha";

			// Act
			var result = query.AppendSkipAndTake(skip, take);

			// Assert
			result.Should().Be($"{query}{Environment.NewLine}OFFSET 0 ROWS FETCH NEXT {take} ROWS ONLY");
		}
	}
}
