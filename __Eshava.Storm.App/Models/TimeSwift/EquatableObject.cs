using System;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.Common
{
	public abstract class EquatableObject<T> : IEquatable<T> where T : class, IIdentifier
	{
		protected abstract int HashCode { get; }
		public abstract Guid? Id { get; set; }

		public override int GetHashCode()
		{
			return HashCode;
		}

		public override bool Equals(object obj)
		{
			if ((obj as T) == null)
			{
				return false;
			}

			return Id.Equals(((T)obj).Id);
		}

		public bool Equals(T obj)
		{
			return Id.Equals(obj?.Id);
		}
	}
}