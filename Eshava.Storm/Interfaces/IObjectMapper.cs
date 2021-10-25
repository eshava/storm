namespace Eshava.Storm.Interfaces
{
	public interface IObjectMapper
	{
		/// <summary>
		/// Creates an object instance and maps all column values of the given alias to the object instance
		/// If <see cref="T"/> is no class, the parameter table alias will be ignored
		/// </summary>
		/// <param name="tableAlias">Single alias or comma separeted list</param>
		/// <returns></returns>
		T Map<T>(string tableAlias = null);

		/// <summary>
		/// Gets the value for the given column name
		/// </summary>
		/// <typeparam name="T">Must not be class, custom data types are possible</typeparam>
		/// <param name="columnName">Column name</param>
		/// <param name="tableAlias">Single alias</param>
		/// <returns></returns>
		T GetValue<T>(string columnName, string tableAlias = null);
	}
}