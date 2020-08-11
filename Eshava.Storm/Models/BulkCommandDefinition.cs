using System.Collections.Generic;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Eshava.Storm.Models
{
	public class BulkCommandDefinition<T> where T : class
	{
		public BulkCommandDefinition(
			SqlConnection connection,
			IEnumerable<T> entities,
			SqlTransaction transaction = null,
			int? commandTimeout = null,
			CancellationToken cancellationToken = default)
		{
			Connection = connection;
			Transaction = transaction;
			CommandTimeout = commandTimeout;
			CancellationToken = cancellationToken;
			Entities = entities;
		}

		/// <summary>
		/// The active connection for the command
		/// </summary>
		public SqlConnection Connection { get; }

		/// <summary>
		/// The active transaction for the command
		/// </summary>
		public SqlTransaction Transaction { get; }

		/// <summary>
		/// The effective timeout for the command
		/// </summary>
		public int? CommandTimeout { get; }

		/// <summary>
		/// For asynchronous operations, the cancellation-token
		/// </summary>
		public CancellationToken CancellationToken { get; }
		
		public IEnumerable<T> Entities { get; set; }
	}
}
