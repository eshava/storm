#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;

namespace Eshava.Storm.Handler
{

	[InterpolatedStringHandler]
	public ref struct SqlInterpolatedStringHandler
	{
		private DefaultInterpolatedStringHandler _innerHandler;
		private Dictionary<string, object> _parameters;
		private string _prefix;
		private bool _hasAtSymbol;

		public readonly IReadOnlyDictionary<string, object> Parameters => new ReadOnlyDictionary<string, object>(_parameters);

		public SqlInterpolatedStringHandler(int literalLength, int formattedCount)
		{
			_parameters = new Dictionary<string, object>(formattedCount);
			_innerHandler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
			_prefix = Guid.NewGuid().ToString()[..4];
		}

		public void AppendLiteral(string literal)
		{
			_innerHandler.AppendLiteral(literal);
			_hasAtSymbol = literal.Last() == '@';
		}

		public void AppendFormatted<T>(T value)
		{
			if (_hasAtSymbol)
			{
				var paramName = _prefix + "_" + (_parameters.Count + 1).ToString();

				_parameters.Add(paramName, value);
				_innerHandler.AppendLiteral(paramName);
				_hasAtSymbol = false;
			}
			else
			{
				_innerHandler.AppendLiteral(value?.ToString());
			}
		}

		public string ToStringAndClear() => _innerHandler.ToStringAndClear();
		public override string ToString() => _innerHandler.ToString();
	}
}

#endif