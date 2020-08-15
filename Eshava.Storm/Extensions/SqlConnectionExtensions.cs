using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eshava.Storm.Engines;
using Eshava.Storm.Models;
using Microsoft.Data.SqlClient;

namespace Eshava.Storm
{
	public static class SqlConnectionExtensions
	{
		public static Task BulkInsertAsync<T>(this SqlConnection connection, IEnumerable<T> entitiesToInsert, SqlTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var engine = new BulkCRUDCommandEngine();
			var command = new BulkCommandDefinition<T>(
				connection,
				entitiesToInsert,
				transaction,
				commandTimeout,
				cancellationToken
			);

			return engine.BulkInsertAsync(command);
		}
	}
}