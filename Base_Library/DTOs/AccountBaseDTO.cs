using System.ComponentModel.DataAnnotations;

namespace Base_Library.DTOs
{
	public class AccountBaseDTO
	{
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		[Required]
		public string? EmailAddress { get; set; }

		[DataType(DataType.Password)]
		[Required]
		public string? Password { get; set; }
	}
}
