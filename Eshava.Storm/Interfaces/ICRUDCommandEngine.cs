using System.Collections.Generic;
using Eshava.Storm.Models;

namespace Eshava.Storm.Interfaces
{
	internal interface ICRUDCommandEngine
	{
		void ProcessInsertRequest<T>(CommandDefinition<T> commandDefinition) where T : class;

		void ProcessUpdateRequest<T>(CommandDefinition<T> commandDefinition, object partialEntity = null, IEnumerable<KeyValuePair<string, object>> patchProperties = null) where T : class;

		void ProcessDeleteRequest<T>(CommandDefinition<T> commandDefinition) where T : class;

		void ProcessQueryEntityRequest<T>(CommandDefinition<T> commandDefinition, object id) where T : class;
	}
}