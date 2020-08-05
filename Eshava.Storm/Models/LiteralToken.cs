using System;
using System.Collections.Generic;

namespace Eshava.Storm.Models
{
	/// <summary>
	/// Represents a placeholder for a value that should be replaced as a literal value in the resulting sql
	/// </summary>
	internal struct LiteralToken
	{
		internal static IEnumerable<LiteralToken> None => Array.Empty<LiteralToken>();

		internal LiteralToken(string token, string member)
		{
			Token = token;
			Member = member;
		}

		/// <summary>
		/// The text in the original command that should be replaced
		/// </summary>
		public string Token { get; }

		/// <summary>
		/// The name of the member referred to by the token
		/// </summary>
		public string Member { get; }
	}
}