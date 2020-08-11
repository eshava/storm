using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using Eshava.Storm.Extensions;
using Eshava.Storm.Interfaces;
using Eshava.Storm.Models;

namespace Eshava.Storm.Engines
{
	internal class CRUDCommandEngine
	{
		private readonly IObjectGenerator _objectGenerator;

		public CRUDCommandEngine(IObjectGenerator objectGenerator)
		{
			_objectGenerator = objectGenerator;
		}

		public void ProcessInsertRequest<T>(CommandDefinition<T> commandDefinition) where T : class
		{
			var type = CheckCommandConditions(commandDefinition, "insert");
			var tableName = GetTableName(type);
			var keyColumns = GetKeyColumns(type);

			if (!keyColumns.Any())
			{
				throw new ArgumentException("At least one key column property must be defined.");
			}

			var properties = GetProperties(type, commandDefinition.Entity);

			var sql = new StringBuilder();
			var sqlColumns = new StringBuilder();
			var sqlValues = new StringBuilder();

			var parameters = new List<KeyValuePair<string, object>>();
			var firstColumn = true;
			foreach (var property in properties)
			{
				if (keyColumns.Any(key =>
					   key.Property.Name == property.Property.Name
					&& property.Prefix.IsNullOrEmpty()
					&& key.DatabaseGenerated))
				{
					continue;
				}

				if (firstColumn)
				{
					firstColumn = false;
				}
				else
				{
					sqlColumns.Append(",");
					sqlValues.Append(",");
				}

				sqlValues.Append("@");
				if (!property.Prefix.IsNullOrEmpty())
				{
					sqlColumns.Append(property.Prefix);
					sqlValues.Append(property.Prefix);
				}

				var columnName = GetColumnName(property.Property);
				sqlColumns.Append(columnName.Column);
				sqlValues.Append(columnName.Property);

				parameters.Add(new KeyValuePair<string, object>($"{property.Prefix}{columnName.Property}", property.Property.GetValue(property.Entity)));
			}

			sql.Append("INSERT INTO ");
			sql.Append(tableName);
			sql.Append("(");
			sql.Append(sqlColumns.ToString());
			sql.AppendLine(")");
			sql.Append("VALUES (");
			sql.Append(sqlValues.ToString());
			sql.AppendLine(");");

			if (keyColumns.Count() == 1 && keyColumns.First().DatabaseGenerated)
			{
				sql.AppendLine("SELECT SCOPE_IDENTITY();");
			}
			else
			{
				sql.Append("SELECT @");
				sql.Append(keyColumns.First().Property.Name);
				sql.AppendLine(";");
			}

			commandDefinition.UpdateCommand(sql.ToString(), parameters);
		}

		public void ProcessUpdateRequest<T>(CommandDefinition<T> commandDefinition, object partialEntity = null) where T : class
		{
			var type = CheckCommandConditions(commandDefinition, "update", partialEntity);
			var tableName = GetTableName(type);
			var keyColumns = GetKeyColumns(type, partialEntity?.GetType());

			if (!keyColumns.Any())
			{
				throw new ArgumentException("At least one key column property must be defined.");
			}

			if (partialEntity != default)
			{
				commandDefinition.Entity = _objectGenerator.CreateEmptyInstance<T>();
			}

			var properties = GetProperties(type, commandDefinition.Entity, partialEntity: partialEntity);

			var sql = new StringBuilder();
			var parameters = new List<KeyValuePair<string, object>>();

			sql.Append("UPDATE ");
			sql.AppendLine(tableName);
			sql.AppendLine(" SET");

			var firstColumn = true;
			foreach (var property in properties)
			{
				if (keyColumns.Any(key =>
					   key.Property.Name == property.Property.Name
					&& property.Prefix.IsNullOrEmpty()))
				{
					continue;
				}

				sql.Append("\t");
				if (firstColumn)
				{
					firstColumn = false;
					sql.Append(" ");
				}
				else
				{
					sql.Append(",");
				}

				if (!property.Prefix.IsNullOrEmpty())
				{
					sql.Append(property.Prefix);
				}

				var columnName = GetColumnName(property.Property);

				sql.Append(columnName.Column);
				sql.Append(" = @");

				if (!property.Prefix.IsNullOrEmpty())
				{
					sql.Append(property.Prefix);
				}
				sql.AppendLine(columnName.Property);

				parameters.Add(new KeyValuePair<string, object>($"{property.Prefix}{columnName.Property}", property.Property.GetValue(property.Entity)));
			}

			AppendWhereCondition(new WhereCondition
			{
				Query = sql,
				TableName = tableName,
				Parameters = parameters,
				Properties = keyColumns
			},
			partialEntity ?? commandDefinition.Entity);

			commandDefinition.UpdateCommand(sql.ToString(), parameters);
		}

		public void ProcessDeleteRequest<T>(CommandDefinition<T> commandDefinition) where T : class
		{
			var type = CheckCommandConditions(commandDefinition, "delete");
			var tableName = GetTableName(type);
			var keyColumns = GetKeyColumns(type);

			if (!keyColumns.Any())
			{
				throw new ArgumentException("At least one key column property must be defined.");
			}

			var sql = new StringBuilder();
			var parameters = new List<KeyValuePair<string, object>>();

			sql.Append("DELETE FROM ");
			sql.AppendLine(tableName);
			AppendWhereCondition(new WhereCondition
			{
				Query = sql,
				TableName = tableName,
				Parameters = parameters,
				Properties = keyColumns
			},
			commandDefinition.Entity);

			commandDefinition.UpdateCommand(sql.ToString(), parameters);
		}

		private void AppendWhereCondition(WhereCondition condition, object entity)
		{
			condition.Query.Append("WHERE ");

			var firstKey = true;
			foreach (var property in condition.Properties)
			{
				if (firstKey)
				{
					firstKey = false;
				}
				else
				{
					condition.Query.Append("AND ");
				}

				var columnName = GetColumnName(property.Property);

				condition.Query.Append(condition.TableName);
				condition.Query.Append(".");
				condition.Query.Append(columnName.Column);
				condition.Query.Append(" = @");
				condition.Query.AppendLine(columnName.Property);

				condition.Parameters.Add(new KeyValuePair<string, object>(columnName.Property, property.Property.GetValue(entity)));
			}
		}

		private (string Column, string Property) GetColumnName(PropertyInfo propertyInfo)
		{
			return (propertyInfo.GetColumnName(), propertyInfo.Name);
		}

		private IEnumerable<(PropertyInfo Property, bool DatabaseGenerated)> GetKeyColumns(Type type, Type partialType = null)
		{
			var keyColumns = new List<(PropertyInfo Property, bool Autogenerated)>();
			var propertyInfos = type.GetProperties().Where(p => p.CanRead).ToList();
			var partialPropertyInfos = partialType?.GetProperties().Where(p => p.CanRead).ToList();

			foreach (var propertyInfo in propertyInfos)
			{
				if (!propertyInfo.IsPrímaryKey())
				{
					continue;
				}

				if (partialType == default)
				{
					keyColumns.Add((propertyInfo, HasAutoGeneratedValue(propertyInfo)));

					continue;
				}

				var partialPropertyInfo = partialPropertyInfos.FirstOrDefault(p => p.Name == propertyInfo.Name);
				if (partialPropertyInfo != default)
				{
					keyColumns.Add((partialPropertyInfo, HasAutoGeneratedValue(propertyInfo)));
				}
			}

			if (keyColumns.Any())
			{
				return keyColumns;
			}

			foreach (var propertyInfo in propertyInfos)
			{
				if (propertyInfo.Name.ToLower() != "id")
				{
					continue;
				}

				if (partialType == default)
				{
					keyColumns.Add((propertyInfo, HasAutoGeneratedValue(propertyInfo)));

					break;
				}

				var partialPropertyInfo = partialPropertyInfos.FirstOrDefault(p => p.Name == propertyInfo.Name);
				if (partialPropertyInfo != default)
				{
					keyColumns.Add((partialPropertyInfo, HasAutoGeneratedValue(propertyInfo)));
				}

				break;
			}

			return keyColumns;
		}

		private bool HasAutoGeneratedValue(PropertyInfo propertyInfo)
		{
			var databaseGenerated = propertyInfo.GetCustomAttribute<DatabaseGeneratedAttribute>();
			if (databaseGenerated != default)
			{
				return databaseGenerated.DatabaseGeneratedOption != DatabaseGeneratedOption.None;
			}

			return Settings.DefaultKeyColumnValueGeneration != DatabaseGeneratedOption.None;
		}

		private IEnumerable<(string Prefix, PropertyInfo Property, object Entity)> GetProperties(Type type, object entity, string namePrefix = null, object partialEntity = null)
		{
			var properties = new List<(string Prefix, PropertyInfo Property, object Entity)>();
			var propertyInfos = type.GetProperties().Where(p => p.CanRead).ToList();
			var propertyInfosPartial = partialEntity?.GetType().GetProperties().Where(p => p.CanRead).ToList();

			foreach (var propertyInfo in propertyInfos)
			{
				var propertyType = propertyInfo.PropertyType.GetDataType();

				if (propertyType.IsNoClass())
				{
					if (propertyInfo.IsAutoGenerated())
					{
						continue;
					}

					if (partialEntity == default)
					{
						properties.Add((namePrefix, propertyInfo, entity));
					}
					else
					{
						var partialPropertyInfo = propertyInfosPartial.FirstOrDefault(p => p.Name == propertyInfo.Name);
						if (partialPropertyInfo != default)
						{
							properties.Add((namePrefix, partialPropertyInfo, partialEntity));
						}
					}

					continue;
				}

				if (!propertyType.IsClass())
				{
					continue;
				}

				if (propertyInfo.IsOwnsOne())
				{
					var ownsOneEntity = propertyInfo.GetValue(entity);

					if (partialEntity == default)
					{
						if (ownsOneEntity == default)
						{
							continue;
						}

						properties.AddRange(GetProperties(propertyInfo.PropertyType, ownsOneEntity, $"{namePrefix}{propertyInfo.Name}_"));

						continue;
					}

					var partialPropertyInfo = propertyInfosPartial.FirstOrDefault(p => p.Name == propertyInfo.Name);
					if (partialPropertyInfo == default)
					{
						continue;
					}

					var ownsOneEntityPartial = partialPropertyInfo.GetValue(partialEntity);
					if (ownsOneEntityPartial == default)
					{
						continue;
					}

					properties.AddRange(GetProperties(propertyInfo.PropertyType, ownsOneEntity, $"{namePrefix}{propertyInfo.Name}_", ownsOneEntityPartial));

					continue;
				}

				propertyInfo.PropertyType.LookupDbType("", false, out var _);
				if (TypeHandlerMap.Map.ContainsKey(propertyInfo.PropertyType))
				{
					if (partialEntity == default)
					{
						properties.Add((namePrefix, propertyInfo, entity));

						continue;
					}

					var partialPropertyInfo = propertyInfosPartial.FirstOrDefault(p => p.Name == propertyInfo.Name);
					if (partialPropertyInfo != default)
					{
						properties.Add((namePrefix, partialPropertyInfo, partialEntity));
					}
				}
			}

			return properties;
		}

		private string GetTableName(Type type)
		{
			var tableAttribute = type.GetCustomAttribute<TableAttribute>();

			if (tableAttribute != default)
			{
				if (!tableAttribute.Schema.IsNullOrEmpty())
				{
					return $"[{tableAttribute.Schema}].[{tableAttribute.Name}]";
				}

				return $"[{tableAttribute.Name}]";
			}

			if (type.Name.ToLower().EndsWith("y"))
			{
				return $"[{type.Name.Substring(0, type.Name.Length - 1)}ies]";
			}

			if (type.Name.ToLower().EndsWith("s") || type.Name.ToLower().EndsWith("x"))
			{
				return $"[{type.Name}es]";
			}

			return $"[{type.Name}s]";
		}

		private Type CheckCommandConditions<T>(CommandDefinition<T> commandDefinition, string action, object partialEntity = null) where T : class
		{
			var type = typeof(T);

			if (type.IsArray || type.ImplementsIEnumerable())
			{
				throw new ArgumentException($"Entity to {action} must be a single instance. No enumeration or array.");
			}

			if (commandDefinition.Entity == default && partialEntity == default)
			{
				throw new ArgumentNullException($"Entity to {action} must not be NULL");
			}

			return type;
		}
	}
}