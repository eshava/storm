using System;
using System.Data;
using Eshava.Storm.Interfaces;
using Eshava.Storm.QueryParameters;

namespace Eshava.Storm.Handler
{
	internal class DataTableHandler : ITypeHandler
	{
		public bool ReadAsByteArray => false;

		public object Parse(Type destinationType, object value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(IDbDataParameter parameter, object value)
		{
			TableParameter.Set(parameter, value as DataTable, null);
		}
	}
}