using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eshava.Storm.Extensions;
using Eshava.Storm.Interfaces;
using Eshava.Storm.MetaData;
using Eshava.Storm.MetaData.Models;
using Eshava.Storm.Models;

namespace Eshava.Storm.Engines
{
	internal class SqliteCRUDCommandEngine : AbstractCRUDCommandEngine, ICRUDCommandEngine
	{
		public SqliteCRUDCommandEngine(
			IObjectGenerator objectGenerator
		) : base(objectGenerator)
		{
		}

		protected override string GetLastInsertedPrimaryKeyQuery() => "SELECT last_insert_rowid();";
	}
}
