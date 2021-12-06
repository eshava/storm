using Eshava.Storm.Interfaces;

namespace Eshava.Storm.Engines
{
	internal class SqlServerCRUDCommandEngine : AbstractCRUDCommandEngine, ICRUDCommandEngine
	{
		public SqlServerCRUDCommandEngine(
			IObjectGenerator objectGenerator
		) : base(objectGenerator)
		{
		}

		protected override string GetLastInsertedPrimaryKeyQuery() => "SELECT SCOPE_IDENTITY();";
	}
}