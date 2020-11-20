using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Eshava.RP365.Models.Data.Enums;
using Eshava.Storm.Handler;
using Eshava.Storm.Interfaces;
using Newtonsoft.Json;

namespace Eshava.Storm.App
{
	public class DicTypeHandler : TypeHandler<Dictionary<string, string>>
	{
		public override Dictionary<string, string> Parse(object value)
		{
			if (value == default)
			{
				return new Dictionary<string, string>();
			};

			return JsonConvert.DeserializeObject<Dictionary<string, string>>(value.ToString());
		}

		public override void SetValue(IDbDataParameter parameter, Dictionary<string, string> value)
		{
			parameter.Value = JsonConvert.SerializeObject(value);
		}
	}
}
