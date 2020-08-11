using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Eshava.Storm.Extensions;
using Eshava.Storm.Models;
using Microsoft.Data.SqlClient;

namespace Eshava.Storm.Engines
{
	internal class BulkCRUDCommandEngine : AbstractCRUDCommandEngine
	{
		public async Task BulkInsertAsync<T>(BulkCommandDefinition<T> commandDefinition) where T : class
		{
			var type = CheckCommandConditions(commandDefinition.Entities, "insert");
			var tableName = GetTableName(type);
			var keyColumns = GetKeyColumns(type);

			if (!keyColumns.Any())
			{
				throw new ArgumentException("At least one key column property must be defined.");
			}

			var sqlBulkCopy = commandDefinition.Transaction == default
				? new SqlBulkCopy(commandDefinition.Connection)
				{
					DestinationTableName = tableName
				}
				: new SqlBulkCopy(commandDefinition.Connection, SqlBulkCopyOptions.Default, commandDefinition.Transaction)
				{
					DestinationTableName = tableName
				};

			if (commandDefinition.CommandTimeout.HasValue)
			{
				sqlBulkCopy.BulkCopyTimeout = commandDefinition.CommandTimeout.Value;
			}

			var properties = GetProperties(type, commandDefinition.Entities.First());
			var dataTable = CreateDataTable(properties, keyColumns, sqlBulkCopy);

			foreach (var entity in commandDefinition.Entities)
			{
				properties = GetProperties(type, entity);

				var row = dataTable.NewRow();

				foreach (var property in properties)
				{
					if (SkipPropertyForInsert(property, keyColumns))
					{
						continue;
					}

					var columnName = GetColumnName(property.PropertyInfo);
					if (property.TypeHandler == default)
					{
						row[columnName.Column] = property.PropertyInfo.GetValue(property.Entity) ?? DBNull.Value;

						continue;
					}

					var cellValue = property.PropertyInfo.GetValue(property.Entity);
					if (cellValue == null)
					{
						row[columnName.Column] = DBNull.Value;
					}
					else
					{
						var sqlParameter = new SqlParameter();
						property.TypeHandler.SetValue(sqlParameter, cellValue);
						row[columnName.Column] = sqlParameter.Value;
					}
				}

				dataTable.Rows.Add(row);
			}

			await sqlBulkCopy.WriteToServerAsync(dataTable, commandDefinition.CancellationToken);
		}

		private DataTable CreateDataTable(IEnumerable<Property> properties, IEnumerable<KeyProperty> keyColumns, SqlBulkCopy sqlBulkCopy)
		{
			var dataTable = new DataTable();
			foreach (var property in properties)
			{
				if (SkipPropertyForInsert(property, keyColumns))
				{
					continue;
				}

				var columnName = GetColumnName(property.PropertyInfo);
				sqlBulkCopy.ColumnMappings.Add(columnName.Column, columnName.Column);

				if (property.TypeHandler == default || !property.TypeHandler.ReadAsByteArray)
				{
					dataTable.Columns.Add(new DataColumn(columnName.Column, property.PropertyInfo.PropertyType.GetDataType()));

					continue;
				}

				dataTable.Columns.Add(new DataColumn(columnName.Column, typeof(SqlBytes)));
			}

			return dataTable;
		}

		private Type CheckCommandConditions<T>(IEnumerable<T> entities, string action, IEnumerable<object> partialEntity = null) where T : class
		{
			var type = typeof(T);

			if (type.IsArray || type.ImplementsIEnumerable())
			{
				throw new ArgumentException($"Entity to {action} must be a single instance. No enumeration or array.");
			}

			if ((entities == default || !entities.Any()) && (partialEntity == default || !partialEntity.Any()))
			{
				throw new ArgumentNullException($"Entity to {action} must not be NULL");
			}

			return type;
		}
	}
}