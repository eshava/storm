using System;
using Eshava.Storm.MetaData.Enums;

namespace Eshava.Storm.MetaData.Models
{
	internal class OwnsOneEntity : AbstractEntity
	{
		internal OwnsOneEntity(Type type, ConfigurationSource configurationSource) : base(type, configurationSource)
		{

		}
	}
}