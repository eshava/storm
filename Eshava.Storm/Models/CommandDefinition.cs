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
		public string CommandText { get; protected set; }

		/// <summary>
		/// The parameters associated with the command
		/// </summary>
		public object Parameters { get; protected set; }

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

	internal class CommandDefinition<T> : CommandDefinition where T : class
	{
		public CommandDefinition(
		IDbConnection connection,
		T entity,
		IDbTransaction transaction = null,
		int? commandTimeout = null,
		CancellationToken cancellationToken = default)
			: base(connection, null, transaction: transaction, commandTimeout: commandTimeout, cancellationToken: cancellationToken)
		{
			Entity = entity;
		}

		public T Entity { get; set; }

		public void UpdateCommand(string query, object parameters)
		{
			CommandText = query;
			Parameters = parameters;
		}
	}
}