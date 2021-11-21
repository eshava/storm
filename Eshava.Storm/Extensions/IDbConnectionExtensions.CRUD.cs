using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Eshava.Storm.Engines;
using Eshava.Storm.Models;

namespace Eshava.Storm
{
	public static partial class IDbConnectionExtensions
	{
		private static readonly Dictionary<string, Func<Interfaces.IObjectGenerator, Interfaces.ICRUDCommandEngine>> _commandEngines = new Dictionary<string, Func<Interfaces.IObjectGenerator, Interfaces.ICRUDCommandEngine>>
		{
			{ "sqlconnection", objectGenerator => new SqlServerCRUDCommandEngine(objectGenerator) },
			{ "sqliteconnection", objectGenerator => new SqliteCRUDCommandEngine(objectGenerator) }
		};

		private static readonly Func<Interfaces.IObjectGenerator, Interfaces.ICRUDCommandEngine> _defaultCRUDCommandEngine = objectGenerator => new SqlServerCRUDCommandEngine(objectGenerator);

		private static string GetConnectionTypeName(IDbConnection dbConnection) => dbConnection.GetType().Name.ToLowerInvariant();

		private static Interfaces.ICRUDCommandEngine GetCRUDCommandEngine(this IDbConnection connection, Interfaces.IObjectGenerator objectGenerator)
		{
			return (_commandEngines.TryGetValue(GetConnectionTypeName(connection), out var abstractCRUDEngine)
				? abstractCRUDEngine
				: _defaultCRUDCommandEngine)(objectGenerator);
		}

		public static Task<K> InsertAsync<T, K>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var commandDefinition = new CommandDefinition<T>(
				connection,
				entityToInsert,
				transaction,
				commandTimeout,
				cancellationToken);


			connection.GetCRUDCommandEngine(null).ProcessInsertRequest(commandDefinition);

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

			connection.GetCRUDCommandEngine(null).ProcessUpdateRequest(commandDefinition);

			var result = await new SqlEngine().ExecuteAsync(commandDefinition);

			return result == 1;
		}

		public static async Task<bool> UpdatePartialAsync<T>(this IDbConnection connection, object entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var commandDefinition = new CommandDefinition<T>(
				connection,
				null,
				transaction,
				commandTimeout,
				cancellationToken);

			var objectMapper = new ObjectMapper(null, null);
			connection.GetCRUDCommandEngine(objectMapper).ProcessUpdateRequest(commandDefinition, partialEntity: entityToUpdate);

			var result = await new SqlEngine().ExecuteAsync(commandDefinition);

			return result == 1;
		}

		public static async Task<bool> UpdatePatchAsync<T>(this IDbConnection connection, IEnumerable<KeyValuePair<string, object>> propertiesToUpdate, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var commandDefinition = new CommandDefinition<T>(
				connection,
				null,
				transaction,
				commandTimeout,
				cancellationToken);

			var objectMapper = new ObjectMapper(null, null);
			connection.GetCRUDCommandEngine(objectMapper).ProcessUpdateRequest(commandDefinition, patchProperties: propertiesToUpdate);

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

			connection.GetCRUDCommandEngine(null).ProcessDeleteRequest(commandDefinition);

			var result = await new SqlEngine().ExecuteAsync(commandDefinition);

			return result == 1;
		}

		public static Task<T> QueryEntityAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
		{
			var commandDefinition = new CommandDefinition<T>(
				connection,
				null,
				transaction,
				commandTimeout,
				cancellationToken);

			connection.GetCRUDCommandEngine(null).ProcessQueryEntityRequest(commandDefinition, id);

			return new SqlEngine().QueryFirstOrDefaultAsync<T>(commandDefinition, null);
		}
	}
}