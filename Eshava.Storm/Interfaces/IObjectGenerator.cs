namespace Eshava.Storm.Interfaces
{
	internal interface IObjectGenerator
	{
		T CreateEmptyInstance<T>();
	}
}