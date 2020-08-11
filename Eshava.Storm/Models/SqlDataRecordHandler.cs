using System;
using System.Collections.Generic;
using System.Data;
using Eshava.Storm.Interfaces;
using Eshava.Storm.QueryParameters;
using Microsoft.Data.SqlClient.Server;

namespace Eshava.Storm.Models
{
	internal class SqlDataRecordHandler : ITypeHandler
	{
		public bool ReadAsByteArray => false;

		public object Parse(Type destinationType, object value)
		{
			throw new NotSupportedException();
		}

		public void SetValue(IDbDataParameter parameter, object value)
		{
			SqlDataRecordParameter.Set(parameter, value as IEnumerable<SqlDataRecord>, null);
		}
	}
}