using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Eshava.Storm.MetaData.Models
{
	internal static class EntityCache
	{
		private static readonly ConcurrentDictionary<Type, Entity> _entites = new ConcurrentDictionary<Type, Entity>();

		public static void AddEntity(Entity entity)
		{
			_entites.TryAdd(entity.Type, entity);
		}

		public static Entity GetEntity(Type type)
		{
			if (_entites.TryGetValue(type, out var entity))
			{
				return entity;
			}

			return default;
		}
	}
}