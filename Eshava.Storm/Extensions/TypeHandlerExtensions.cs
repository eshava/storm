using Eshava.Storm.Handler;
using Eshava.Storm.Models;

namespace Eshava.Storm.Extensions
{
	public static class TypeHandlerExtensions
	{
		/// <summary>
		/// Configure the specified type to be processed by a custom handler.
		/// </summary>
		/// <typeparam name="T">The type to handle.</typeparam>
		/// <param name="handler">The handler for the type <typeparamref name="T"/>.</param>
		public static void AddTypeHandler<T>(this TypeHandler<T> handler)
		{
			TypeHandlerMap.Add(typeof(T), handler);
		}
	}
}