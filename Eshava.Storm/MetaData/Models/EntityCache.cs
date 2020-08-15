using System;
using System.Collections.Generic;

namespace Eshava.Storm.MetaData.Models
{
	internal static class EntityCache
	{
		private static readonly Dictionary<Type, Entity> _entites = new Dictionary<Type, Entity>();

		public static void AddEntity(Entity entity)
		{
			_entites.Add(entity.Type, entity);
		}

		public static Entity GetEntity(Type type)
		{
			if (!_entites.ContainsKey(type))
			{
				return default;
			}

			return _entites[type];
		}
	}
}