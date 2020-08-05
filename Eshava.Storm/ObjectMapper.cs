using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Eshava.Storm.Attributes;
using Eshava.Storm.Extensions;
using Eshava.Storm.Interfaces;
using Eshava.Storm.Models;

namespace Eshava.Storm
{
	internal class ObjectMapper : IObjectMapper
	{
		private static readonly Type _typeOfString = typeof(string);
		private static readonly Type _typeOfGuid = typeof(Guid);
		private static readonly Type _typeOfDateTime = typeof(DateTime);
		private static readonly Type _typeOfByteArray = typeof(byte[]);


		private readonly DbDataReader _reader;
		private readonly Dictionary<string, string> _tableAliases;
		private readonly DataTypeMapper _dataTypeMapper;
		private Dictionary<string, int> _aliasOccurrences;
		private Dictionary<string, IList<int>> _columnCache;
		private IEnumerable<string> _resultTableNames;

		public ObjectMapper(DbDataReader reader, string sql)
		{
			_dataTypeMapper = new DataTypeMapper();
			_reader = reader;
			_tableAliases = sql.GetTableAliases();
			CalculateTableAliasUsage(sql);
		}

		public T Map<T>(string tableAlias = null)
		{
			if (ShouldMapClass<T>())
			{
				CalculateColumnCache();

				var resultObject = Activator.CreateInstance<T>();
				var requestedTableNames = GetTableNamesFromAlias(tableAlias);
				var readerAccessItems = new List<ReaderAccessItem>();

				PreProcessProperties(readerAccessItems, resultObject, requestedTableNames);

				ExecuteReaderAccessItems(readerAccessItems);

				return resultObject;
			}

			var cellValue = _reader[0];

			return _dataTypeMapper.Map<T>(cellValue);
		}


		private void PreProcessProperties(IList<ReaderAccessItem> readerAccessItems,  object instance, IEnumerable<(string Alias, string TableName)> requestedTableNames, string columnPrefix = "")
		{
			var propertyInfos = GetProperties(instance.GetType());

			foreach (var propertyInfo in propertyInfos)
			{
				var propertyType = propertyInfo.PropertyType.GetDataType();
				if (propertyType.IsClass())
				{
					var ownsOne = propertyInfo.GetCustomAttribute<OwnsOneAttribute>();
					if (ownsOne != default)
					{
						var ownsOneInstance = Activator.CreateInstance(propertyType);
						propertyInfo.SetValue(instance, ownsOneInstance);
						PreProcessProperties(readerAccessItems, ownsOneInstance, requestedTableNames, GetPropertyName(propertyInfo).ToLower() + "_");
					}
				}

				var columnName = columnPrefix + GetPropertyName(propertyInfo).ToLower();

				if (!_resultTableNames.Any() || !requestedTableNames.Any())
				{
					// No result analyse result available
					if (!_columnCache.ContainsKey(columnName))
					{
						var columnNew = _columnCache.Keys.FirstOrDefault(c => c.EndsWith("." + columnName));
						if (!columnNew.IsNullOrEmpty())
						{
							columnName = columnNew;
						}
					}

					if (_columnCache.ContainsKey(columnName))
					{
						readerAccessItems.Add(new ReaderAccessItem
						{
							Ordinal = _columnCache[columnName].Last(),
							Instance = instance,
							PropertyInfo = propertyInfo
						});
					}

					continue;
				}

				var columnFound = false;
				foreach (var requestedTableName in requestedTableNames)
				{
					var fullColumnName = $"{requestedTableName.TableName}.{columnName}";

					if (!_columnCache.ContainsKey(fullColumnName))
					{
						continue;
					}

					columnFound = true;
					if (!_aliasOccurrences.ContainsKey(requestedTableName.Alias))
					{
						if (requestedTableName.Alias == requestedTableName.TableName)
						{
							// Alias is an table name
							readerAccessItems.Add(new ReaderAccessItem
							{
								Ordinal = _columnCache[fullColumnName].Last(),
								Instance = instance,
								PropertyInfo = propertyInfo
							});
						}

						// Skip property if alias is unknown

						continue;
					}

					var aliasOccurrence = _aliasOccurrences[requestedTableName.Alias];
					if (aliasOccurrence >= _columnCache[fullColumnName].Count)
					{
						// Skip if correct column occurrence could not be found

						continue;
					}

					readerAccessItems.Add(new ReaderAccessItem
					{
						Ordinal = _columnCache[fullColumnName][aliasOccurrence],
						Instance = instance,
						PropertyInfo = propertyInfo
					});
				}

				if (!columnFound)
				{
					var fullColumnName = $"none.{columnName}";

					if (_columnCache.ContainsKey(fullColumnName))
					{
						readerAccessItems.Add(new ReaderAccessItem
						{
							Ordinal = _columnCache[fullColumnName].Last(),
							Instance = instance,
							PropertyInfo = propertyInfo
						});
					}
				}
			}
		}

		private string GetPropertyName(PropertyInfo propertyInfo)
		{
			var column = propertyInfo.GetCustomAttribute<ColumnAttribute>();
			if (column != default)
			{
				return column.Name;
			}

			return propertyInfo.Name;
		}

		private IEnumerable<PropertyInfo> GetProperties(Type dataType)
		{
			var propertyInfos = dataType.GetProperties();

			foreach (var propertyInfo in propertyInfos)
			{
				if (!propertyInfo.CanWrite)
				{
					continue;
				}

				var propertyType = propertyInfo.PropertyType.GetDataType();

				if (IsNoClass(propertyType))
				{
					yield return propertyInfo;
				}

				if (propertyType.IsClass())
				{
					var ownsOne = propertyInfo.GetCustomAttribute<OwnsOneAttribute>();
					if (ownsOne != default)
					{
						yield return propertyInfo;
					}
				}
			}
		}

		private IEnumerable<(string Alias, string TableName)> GetTableNamesFromAlias(string tableAlias)
		{
			if (tableAlias.IsNullOrEmpty())
			{
				return Array.Empty<(string Alias, string TableName)>();
			}

			var tableAliases = new List<(string Alias, string TableName)>();
			foreach (var alias in tableAlias.Split(','))
			{
				var tableAliasName = alias.Trim().CleanTableName();

				if (_tableAliases.ContainsKey(tableAliasName))
				{
					tableAliases.Add((tableAliasName, _tableAliases[tableAliasName]));
				}
				else if (_tableAliases.Values.Any(t => t == tableAliasName))
				{
					tableAliases.Add((tableAliasName, tableAliasName));
				}
			}

			return tableAliases;
		}

		private void CalculateTableAliasUsage(string sql)
		{
			sql = sql.Replace("\r", "")
					 .Replace("\n", "")
					 .Replace("\t", " ")
					 .ToLower();

			var aliasOccurrences = _tableAliases
				.Select(pair => (Alias: pair.Key, Table: pair.Value, Index: sql.IndexOf(pair.Key + ".")))
				.GroupBy(t => t.Table);

			_aliasOccurrences = new Dictionary<string, int>();
			foreach (var tableGroup in aliasOccurrences)
			{
				var aliasOccurrencesForTable = tableGroup.OrderBy(t => t.Index).ToList();
				for (var index = 0; index < aliasOccurrencesForTable.Count; index++)
				{
					_aliasOccurrences.Add(aliasOccurrencesForTable[index].Alias, index);
				}
			}
		}

		private void CalculateColumnCache()
		{
			if (_columnCache != default)
			{
				return;
			}

			var schemaTable = default(DataTable);
			if (_reader.CanGetColumnSchema())
			{
				schemaTable = _reader.GetSchemaTable();
			}

			var columnCache = new Dictionary<string, IList<int>>();
			var resultTableNames = new List<string>();

			if (schemaTable == default)
			{
				for (var columnOrdinal = 0; columnOrdinal < _reader.FieldCount; columnOrdinal++)
				{
					var columnName = _reader.GetName(columnOrdinal).ToLower();

					if (columnCache.ContainsKey(columnName))
					{
						if (!Settings.IgnoreDuplicatedColumns)
						{
							columnCache[columnName].Add(columnOrdinal);
						}
					}
					else
					{
						columnCache.Add(columnName, new List<int> { columnOrdinal });
					}
				}
			}
			else
			{
				for (var columnOrdinal = 0; columnOrdinal < _reader.FieldCount; columnOrdinal++)
				{
					foreach (DataRow row in schemaTable.Rows)
					{
						if (Convert.ToInt32(row["ColumnOrdinal"]) == columnOrdinal)
						{
							var tableName = row["BaseTableName"]?.ToString();
							var columnName = row["ColumnName"]?.ToString();

							if (tableName.IsNullOrEmpty())
							{
								tableName = "none";
							}

							if (columnName.IsNullOrEmpty())
							{
								columnName = "none";
							}

							var name = $"{tableName.ToLower()}.{columnName.ToLower()}";

							if (columnCache.ContainsKey(name))
							{
								columnCache[name].Add(columnOrdinal);
							}
							else
							{
								columnCache.Add(name, new List<int> { columnOrdinal });
							}

							break;
						}
					}

					foreach (DataRow row in schemaTable.Rows)
					{
						resultTableNames.Add(row["BaseTableName"].ToString());
					}
				}
			}

			_columnCache = columnCache;
			_resultTableNames = resultTableNames.Distinct().ToList();
		}

		private bool ShouldMapClass<T>()
		{
			return !IsNoClass(typeof(T));
		}

		private bool IsNoClass(Type type)
		{
			var propertyType = type.GetDataType();

			if (propertyType.IsPrimitive
				|| propertyType.IsEnum
				|| propertyType == _typeOfString
				|| propertyType == _typeOfGuid
				|| propertyType == _typeOfDateTime
				|| propertyType == _typeOfByteArray)
			{
				return true;
			}

			return false;
		}

		private void ExecuteReaderAccessItems(IEnumerable<ReaderAccessItem> readerAccessItems) {
			readerAccessItems = readerAccessItems.OrderBy(rai => rai.Ordinal).ToList();

			foreach (var item in readerAccessItems)
			{
				var cellValue = _reader[item.Ordinal];
				item.PropertyInfo.SetValue(item.Instance, _dataTypeMapper.Map(item.PropertyInfo.PropertyType, cellValue));
			}
		}
	}
}