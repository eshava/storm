namespace Eshava.Storm.MetaData.Enums
{
	internal enum ConfigurationSource
	{
		/// <summary>
		/// Indicates that the model element was explicitly specified using the fluent API
		/// </summary>
		Explicit,

		/// <summary>
		/// Indicates that the model element was specified through use of a .NET attribute (data annotation).
		/// </summary>
		DataAnnotation,

		/// <summary>
		/// Indicates that the model element was specified by convention via the EF Core model building conventions.
		/// </summary>
		Convention
	}
}