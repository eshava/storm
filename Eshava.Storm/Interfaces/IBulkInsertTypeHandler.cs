using System;
using System.Data;

namespace Eshava.Storm.Interfaces
{
	/// <summary>
	/// Implement this interface to perform custom type-based parameter handling and value parsing
	/// </summary>
	public interface IBulkInsertTypeHandler
	{
		Type GetDateType();
	}
}