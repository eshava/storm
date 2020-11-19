using System;
using System.ComponentModel.DataAnnotations;
using TimeSwift.Models.Data.Interfaces;

namespace TimeSwift.Models.Data.Common
{
	public abstract class EditorDataModel<T> : EquatableObject<T> where T : class, IIdentifier
	{
		[Required]
		[MaxLength(100)]
		public string CreatedBy { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime CreatedAt { get; set; }

		[Required]
		[MaxLength(100)]
		public string ModifiedBy { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime ModifiedAt { get; set; }
	}
}