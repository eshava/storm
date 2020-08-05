using Eshava.Storm.Constants;
using Eshava.Storm.Extensions;
using Eshava.Storm.Interfaces;
using Eshava.Storm.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Eshava.Storm
{
	internal class ParameterCollector
	{
		private readonly Dictionary<string, ParameterInfo> _parameters = new Dictionary<string, ParameterInfo>();

		/// <summary>
		/// Construct a parameter collector
		/// </summary>
		public ParameterCollector()
		{
			RemoveUnused = true;
		}

		/// <summary>
		/// Construct a parameter collector
		/// </summary>
		/// <param name="parameter">Can be an anonymous type or a <see cref="ParameterCollector"/></param>
		public ParameterCollector(object parameter)
		{
			RemoveUnused = true;
			AddParameter(parameter);
		}

		public bool RemoveUnused { get; set; }

		public void AddParameter(object parameter)
		{
			if (parameter as object == null)
			{
				return;
			}

			var collector = parameter as ParameterCollector;
			if (collector != null)
			{
				if ((collector._parameters?.Count ?? 0) == 0)
				{
					return;
				}

				foreach (var internalParameter in collector._parameters)
				{
					Add(internalParameter);
				}

				return;
			}

			var dictionary = parameter as IEnumerable<KeyValuePair<string, object>>;
			if (dictionary != null)
			{
				foreach (var keyValuePair in dictionary)
				{
					Add(keyValuePair.Key, keyValuePair.Value, null, null, null);
				}
			}

			var parameterPropertyInfos = parameter.GetType().GetProperties();
			foreach (var propertyInfo in parameterPropertyInfos)
			{
				Add(propertyInfo.Name, propertyInfo.GetValue(parameter), null, null, null);
			}
		}

		public void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
		{
			_parameters[name.Clean()] = new ParameterInfo
			{
				Name = name,
				Value = value,
				ParameterDirection = direction ?? ParameterDirection.Input,
				DbType = dbType,
				Size = size
			};
		}

		public void AddParameters(IDbCommand command, Identity identity)
		{
			foreach (var parameter in _parameters.Values)
			{
				var dbType = parameter.DbType;
				var parameterName = parameter.Name.Clean();
				var isCustomQueryParameter = parameter.Value is ICustomQueryParameter;

				ITypeHandler handler = null;

				if (dbType == null && parameter.Value != null && !isCustomQueryParameter)
				{
					dbType = parameter.Value.GetType().LookupDbType(parameterName, true, out handler);
				}

				if (isCustomQueryParameter)
				{
					((ICustomQueryParameter)parameter.Value).AddParameter(command, parameterName);
				}
				else
				{
					var addParameter = !command.Parameters.Contains(parameterName);
					IDbDataParameter dataParameter;
					
					if (addParameter)
					{
						dataParameter = command.CreateParameter();
						dataParameter.ParameterName = parameterName;
					}
					else
					{
						dataParameter = (IDbDataParameter)command.Parameters[parameterName];
					}

					dataParameter.Direction = parameter.ParameterDirection;
					if (handler == null)
					{
						dataParameter.Value = parameter.Value.SanitizeParameterValue();
						if (dbType != null && dataParameter.DbType != dbType)
						{
							dataParameter.DbType = dbType.Value;
						}
						var s = parameter.Value as string;
						if (s?.Length <= DefaultValues.DBSTRINGDEFAULTLENGTH)
						{
							dataParameter.Size = DefaultValues.DBSTRINGDEFAULTLENGTH;
						}
						if (parameter.Size != null)
						{
							dataParameter.Size = parameter.Size.Value;
						}

						if (parameter.Precision != null)
						{
							dataParameter.Precision = parameter.Precision.Value;
						}

						if (parameter.Scale != null)
						{
							dataParameter.Scale = parameter.Scale.Value;
						}
					}
					else
					{
						if (dbType != null)
						{
							dataParameter.DbType = dbType.Value;
						}

						if (parameter.Size != null)
						{
							dataParameter.Size = parameter.Size.Value;
						}

						if (parameter.Precision != null)
						{
							dataParameter.Precision = parameter.Precision.Value;
						}

						if (parameter.Scale != null)
						{
							dataParameter.Scale = parameter.Scale.Value;
						}

						handler.SetValue(dataParameter, parameter.Value ?? DBNull.Value);
					}

					if (addParameter)
					{
						command.Parameters.Add(dataParameter);
					}
					parameter.AttachedParam = dataParameter;
				}
			}
		}

		private void Add(KeyValuePair<string, ParameterInfo> keyValuePair)
		{
			if (_parameters.ContainsKey(keyValuePair.Key))
			{
				_parameters[keyValuePair.Key] = keyValuePair.Value;
			}
			else
			{
				_parameters.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}