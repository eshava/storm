using System.Data;
using System.Threading;

namespace Eshava.Storm.Models
{
	internal class CommandDefinition
	{
		public CommandDefinition(
			IDbConnection connection,
			string commandText,
			object parameters = null,
			IDbTransaction transaction = null,
			int? commandTimeout = null,
			CommandType? commandType = null,
			CancellationToken cancellationToken = default)
		{
			Connection = connection;
			CommandText = commandText;
			Parameters = parameters;
			Transaction = transaction;
			CommandText = commandText;
			CommandTimeout = commandTimeout;
			CommandType = commandType;
			CancellationToken = cancellationToken;
		}

		/// <summary>
		/// The active connection for the command
		/// </summary>
		public IDbConnection Connection { get; }

		/// <summary>
		/// The command (sql or a stored-procedure name) to execute
		/// </summary>
		public string CommandText { get; }

		/// <summary>
		/// The parameters associated with the command
		/// </summary>
		public object Parameters { get; }

		/// <summary>
		/// The active transaction for the command
		/// </summary>
		public IDbTransaction Transaction { get; }

		/// <summary>
		/// The effective timeout for the command
		/// </summary>
		public int? CommandTimeout { get; }

		/// <summary>
		/// The type of command that the command-text represents
		/// </summary>
		public CommandType? CommandType { get; }

		/// <summary>
		/// For asynchronous operations, the cancellation-token
		/// </summary>
		public CancellationToken CancellationToken { get; }
	}
}