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
	}
}