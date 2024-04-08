

using System.ComponentModel.DataAnnotations;

namespace Base_Library.DTOs
{
	public class RegisterDTO : AccountBaseDTO
	{
		[Required]
		[MinLength(5)]
		[MaxLength(100)]
		public string? FullName { get; set; }

		[Required]
		[MinLength(5)]
		[MaxLength(100)]
		public string? UserName { get; set; }

		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		[Required]
		public string? ConfirmPassword { get; set; }
	}
}
