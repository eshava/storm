using System;
using System.ComponentModel.DataAnnotations;

namespace TimeSwift.Models.Data.BasicInformation.Vehicles
{
	public class DimensionModel
	{
		[Range(0, Int32.MaxValue)]
		public int WidthInMillimeter { get; set; }

		[Range(0, Int32.MaxValue)]
		public int HeightInMillimeter { get; set; }

		[Range(0, Int32.MaxValue)]
		public int LengthInMillimeter { get; set; }
	}
}
