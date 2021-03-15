using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Eshava.Storm.Extensions;
using Eshava.Storm.Interfaces;
using Eshava.Storm.MetaData;
using Eshava.Storm.MetaData.Models;
using Eshava.Storm.Models;

namespace Eshava.Storm
{
	internal class ObjectMapper : IObjectMapper, IObjectGenerator
	{
		private readonly DbDataReader _reader;
		private readonly DataTypeMapper _dataTypeMapper;
		private TableAnalysisResult _tableAnalysisResult;

		public ObjectMapper(DbDataReader reader, string sql)
		{
			_dataTypeMapper = new DataTypeMapper();
			_reader = reader;
			SetTableAnalysisResult(sql);
		}

		public T Map<T>(string tableAlias = null)
		{
			if (ShouldMapClass<T>())
			{
				CalculateColumnCache();

				var resultObject = Activator.CreateInstance<T>();
				var requestedTableNames = GetTableNamesFromAlias(tableAlias);
				var readerAccessItems = new List<ReaderAccessItem>();

				PreProcessProperties(new PreProcessPropertyInformation
				{
					ReaderAccessItems = readerAccessItems,
					Instance = resultObject,
					RequestedTableNames = requestedTableNames
				});

				ExecuteReaderAccessItems(readerAccessItems);

				return resultObject;
			}

			var cellValue = _reader[0];

			return _dataTypeMapper.Map<T>(cellValue);
		}

		public T CreateEmptyInstance<T>()
		{
			return (T)CreateEmptyInstance(typeof(T));
		}

		private object CreateEmptyInstance(Type type)
		{
			var entity = EntityCache.GetEntity(type) ?? TypeAnalyzer.AnalyzeType(type);
			var instance = Activator.CreateInstance(type);

			foreach (var property in entity.GetProperties())
			{
				if (property.IsOwnsOne)
				{
					property.PropertyInfo.SetValue(instance, CreateEmptyInstance(property.Type));
				}
			}

			return instance;
		}

		private void PreProcessProperties(PreProcessPropertyInformation information)
		{
			information.Entity = information.Entity ?? EntityCache.GetEntity(information.Instance.GetType()) ?? TypeAnalyzer.AnalyzeType(information.Instance.GetType());

			foreach (var property in information.Entity.GetProperties())
			{
				if (property.IsOwnsOne)
				{
					var ownsOneInstance = Activator.CreateInstance(property.Type);
					property.PropertyInfo.SetValue(information.Instance, ownsOneInstance);
					PreProcessProperties(new PreProcessPropertyInformation
					{
						ReaderAccessItems = information.ReaderAccessItems,
						Instance = ownsOneInstance,
						RequestedTableNames = information.RequestedTableNames,
						ColumnPrefix = property.ColumnName.ToLower() + "_",
						Entity = property.OwnsOne
					});
				}

				var columnName = information.ColumnPrefix + property.ColumnName.ToLower();

				if (!_tableAnalysisResult.ResultTableNames.Any() || !information.RequestedTableNames.Any())
				{
					// No result analyse result available
					if (!_tableAnalysisResult.ColumnCache.ContainsKey(columnName))
					{
						var columnNew = _tableAnalysisResult.ColumnCache.Keys.FirstOrDefault(c => c.EndsWith("." + columnName));
						if (!columnNew.IsNullOrEmpty())
						{
							columnName = columnNew;
						}
					}

					if (_tableAnalysisResult.ColumnCache.ContainsKey(columnName))
					{
						information.ReaderAccessItems.Add(new ReaderAccessItem
						{
							Ordinal = _tableAnalysisResult.ColumnCache[columnName].Last(),
							Instance = information.Instance,
							PropertyInfo = property.PropertyInfo
						});
					}

					continue;
				}

				var columnFound = false;
				foreach (var requestedTableName in information.RequestedTableNames)
				{
					var fullColumnName = $"{requestedTableName.TableName}.{columnName}";

					if (!_tableAnalysisResult.ColumnCache.ContainsKey(fullColumnName))
					{
						continue;
					}

					columnFound = true;
					if (!_tableAnalysisResult.AliasOccurrences.ContainsKey(requestedTableName.Alias))
					{
						if (requestedTableName.Alias == requestedTableName.TableName)
						{
							// Alias is an table name
							information.ReaderAccessItems.Add(new ReaderAccessItem
							{
								Ordinal = _tableAnalysisResult.ColumnCache[fullColumnName].Last(),
								Instance = information.Instance,
								PropertyInfo = property.PropertyInfo
							});
						}

						// Skip property if alias is unknown

						continue;
					}

					var aliasOccurrence = _tableAnalysisResult.AliasOccurrences[requestedTableName.Alias];
					if (aliasOccurrence >= _tableAnalysisResult.ColumnCache[fullColumnName].Count)
					{
						// Skip if correct column occurrence could not be found

						continue;
					}

					information.ReaderAccessItems.Add(new ReaderAccessItem
					{
						Ordinal = _tableAnalysisResult.ColumnCache[fullColumnName][aliasOccurrence],
						Instance = information.Instance,
						PropertyInfo = property.PropertyInfo
					});
				}

				if (!columnFound)
				{
					var fullColumnName = $"none.{columnName}";

					if (_tableAnalysisResult.ColumnCache.ContainsKey(fullColumnName))
					{
						information.ReaderAccessItems.Add(new ReaderAccessItem
						{
							Ordinal = _tableAnalysisResult.ColumnCache[fullColumnName].Last(),
							Instance = information.Instance,
							PropertyInfo = property.PropertyInfo
						});
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

				if (_tableAnalysisResult.TableAliases.ContainsKey(tableAliasName))
				{
					tableAliases.Add((tableAliasName, _tableAnalysisResult.TableAliases[tableAliasName]));
				}
				else if (_tableAnalysisResult.TableAliases.Values.Any(t => t == tableAliasName))
				{
					tableAliases.Add((tableAliasName, tableAliasName));
				}
			}

			return tableAliases;
		}

		private void SetTableAnalysisResult(string sql)
		{
			if (sql.IsNullOrEmpty())
			{
				return;
			}

			//var sqlHashCode = sql.GetHashCode();
			//var tableAnalysisResult = ReaderInformationCache.GetReaderTableAnalysisResult(sqlHashCode);
			//if (tableAnalysisResult != default)
			//{
			//	_tableAnalysisResult = tableAnalysisResult;

			//	return;
			//}

			var tableAliases = sql.GetTableAliases();
			var aliasOccurrences = CalculateTableAliasUsage(sql, tableAliases);

			var tableAnalysisResult = new TableAnalysisResult
			{
				TableAliases = tableAliases,
				AliasOccurrences = aliasOccurrences
			};

			//ReaderInformationCache.AddTableAnalysisResult(sqlHashCode, tableAnalysisResult);

			_tableAnalysisResult = tableAnalysisResult;
		}

		private Dictionary<string, int> CalculateTableAliasUsage(string sql, Dictionary<string, string> tableAliases)
		{
			if (sql.IsNullOrEmpty())
			{
				return null;
			}

			sql = sql.Replace("\r", "")
					 .Replace("\n", "")
					 .Replace("\t", " ")
					 .ToLower();

			var aliasOccurrences = tableAliases
				.Select(pair => (Alias: pair.Key, Table: pair.Value, Index: sql.IndexOf(pair.Key + ".")))
				.GroupBy(t => t.Table);

			var resultAliasOccurrences = new Dictionary<string, int>();
			foreach (var tableGroup in aliasOccurrences)
			{
				var aliasOccurrencesForTable = tableGroup.OrderBy(t => t.Index).ToList();
				for (var index = 0; index < aliasOccurrencesForTable.Count; index++)
				{
					resultAliasOccurrences.Add(aliasOccurrencesForTable[index].Alias, index);
				}
			}

			return resultAliasOccurrences;
		}

		private void CalculateColumnCache()
		{
			if (_tableAnalysisResult.ColumnCache != default)
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

			_tableAnalysisResult.ColumnCache = columnCache;
			_tableAnalysisResult.ResultTableNames = resultTableNames.Distinct().ToList();
		}

		private bool ShouldMapClass<T>()
		{
			return !typeof(T).IsNoClass();
		}

		private void ExecuteReaderAccessItems(IEnumerable<ReaderAccessItem> readerAccessItems)
		{
			readerAccessItems = readerAccessItems
				.Where(rai => rai.Ordinal >= 0)
				.OrderBy(rai => rai.Ordinal)
				.ToList();

			foreach (var item in readerAccessItems)
			{
				var cellValue = default(object);
				if (TypeHandlerMap.Map.ContainsKey(item.PropertyInfo.PropertyType))
				{
					cellValue = GetValueByTypeHandler(item.PropertyInfo.PropertyType, TypeHandlerMap.Map[item.PropertyInfo.PropertyType], item.Ordinal);
				}
				else
				{
					cellValue = _reader[item.Ordinal];
				}

				item.PropertyInfo.SetValue(item.Instance, _dataTypeMapper.Map(item.PropertyInfo.PropertyType, cellValue));
			}
		}

		private object GetValueByTypeHandler(Type type, ITypeHandler typeHandler, int ordinal)
		{
			if (!typeHandler.ReadAsByteArray)
			{
				return _reader[ordinal];
			}

			if (_reader.IsDBNull(ordinal))
			{
				return null;
			}

			var size = _reader.GetBytes(ordinal, 0, null, 0, 0);
			var result = new byte[size];
			_reader.GetBytes(ordinal, 0, result, 0, result.Length);

			return typeHandler.Parse(type, result);
		}
	}
}