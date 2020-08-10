using System;
using System.Collections.Generic;
using System.Data;
using Eshava.Storm.Handler;
using Eshava.Storm.Interfaces;
using Microsoft.Data.SqlClient.Server;

namespace Eshava.Storm.Models
{
	internal static class TypeHandlerMap
	{
		private static Dictionary<Type, ITypeHandler> _map = new Dictionary<Type, ITypeHandler>();

		internal static Dictionary<Type, ITypeHandler> Map
		{
			get
			{
				if (_map.Count == 0)
				{
					InitiateMap();
				}

				return _map;
			}
		}

		public static void Add(Type type, ITypeHandler typeHandler)
		{
			if (!_map.ContainsKey(type))
			{
				_map.Add(type, typeHandler);
			}
		}

		private static void InitiateMap()
		{
			_map.Clear();
			_map.Add(typeof(DataTable), new DataTableHandler());
			_map.Add(typeof(IEnumerable<SqlDataRecord>), new SqlDataRecordHandler());
		}		
	}
}