using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Eshava.Storm.Engines;
using Eshava.Storm.Models;

namespace Eshava.Storm
{
	public static partial class IDbConnectionExtensions
	{
		public static Task<K> InsertAsync<T, K>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var commandDefinition = new CommandDefinition<T>(
				connection,
				entityToInsert,
				transaction,
				commandTimeout,
				cancellationToken);

			new CRUDCommandEngine().ProcessInsertRequest(commandDefinition);

			return new SqlEngine().ExecuteScalarAsync<K>(commandDefinition);
		}

		public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var commandDefinition = new CommandDefinition<T>(
				connection,
				entityToUpdate,
				transaction,
				commandTimeout,
				cancellationToken);

			new CRUDCommandEngine().ProcessUpdateRequest(commandDefinition);

			var result = await new SqlEngine().ExecuteAsync(commandDefinition);

			return result == 1;
		}

		public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var commandDefinition = new CommandDefinition<T>(
				connection,
				entityToDelete,
				transaction,
				commandTimeout,
				cancellationToken);

			new CRUDCommandEngine().ProcessDeleteRequest(commandDefinition);

			var result = await new SqlEngine().ExecuteAsync(commandDefinition);

			return result == 1;
		}
	}
}