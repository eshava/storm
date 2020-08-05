using System;
using System.Data;
using System.Threading;

namespace Eshava.Storm.Models
{
	internal class CacheInfo
	{
		private int _hitCount;

		public DeserializerState Deserializer { get; set; }
		public Func<IDataReader, object>[] OtherDeserializers { get; set; }
		public Action<IDbCommand, object> ParamReader { get; set; }
		
		public int GetHitCount()
		{
			return Interlocked.CompareExchange(ref _hitCount, 0, 0);
		}

		public void RecordHit()
		{
			Interlocked.Increment(ref _hitCount);
		}
	}
}