using System;

namespace TimeSwift.Models.Data.Enums
{
	[Flags]
	public enum RightTypes
	{
		None = 0,
		Read = 1,
		Create = 2,
		Edit = 4,
		Delete = 8,
		Print = 16
	}
}