

using System.ComponentModel.DataAnnotations;

namespace Base_Library.DTOs
{
	public class RegisterDTO : AccountBase
	{
		[Required]
		[MinLength(5)]
		[MaxLength(100)]
		public string? FullName { get; set; }

		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		[Required]
		public string? ConfirmPassword { get; set; }
	}
}
