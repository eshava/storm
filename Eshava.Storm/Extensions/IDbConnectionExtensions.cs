using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Eshava.Storm.Engines;
using Eshava.Storm.Interfaces;
using Eshava.Storm.Models;

namespace Eshava.Storm
{
	public static class IDbConnectionExtensions
	{
		public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
		{
			var commandDefinition = new CommandDefinition(
				connection,
				sql,
				parameter,
				transaction,
				commandTimeout,
				commandType,
				cancellationToken);


			return new SqlEngine().QueryAsync<T>(commandDefinition, null);
		}

		public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, string sql, Func<IObjectMapper, T> map, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
		{
			var commandDefinition = new CommandDefinition(
				connection,
				sql,
				parameter,
				transaction,
				commandTimeout,
				commandType,
				cancellationToken);


			return new SqlEngine().QueryAsync(commandDefinition, map);
		}


		public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
		{
			var commandDefinition = new CommandDefinition(
				connection,
				sql,
				parameter,
				transaction,
				commandTimeout,
				commandType,
				cancellationToken);


			return new SqlEngine().QueryFirstOrDefaultAsync<T>(commandDefinition, null);
		}

		public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection connection, string sql, Func<IObjectMapper, T> map, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
		{
			var commandDefinition = new CommandDefinition(
				connection,
				sql,
				parameter,
				transaction,
				commandTimeout,
				commandType,
				cancellationToken);

			return new SqlEngine().QueryFirstOrDefaultAsync(commandDefinition, map);
		}


		public static Task<T> ExecuteScalarAsync<T>(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
		{
			var commandDefinition = new CommandDefinition(
				connection,
				sql,
				parameter,
				transaction,
				commandTimeout,
				commandType,
				cancellationToken);

			return new SqlEngine().ExecuteScalarAsync<T>(commandDefinition);
		}

		public static Task<int> ExecuteAsync(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
		{
			var commandDefinition = new CommandDefinition(
				connection,
				sql,
				parameter,
				transaction,
				commandTimeout,
				commandType,
				cancellationToken);

			return new SqlEngine().ExecuteAsync(commandDefinition);
		}
	}
}