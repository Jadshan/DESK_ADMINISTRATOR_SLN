
namespace Base_Library.Entities
{
	public class AppUser : BaseEntity
	{
		public string? UserName { get; set; }
		public string? UserEmail { get; set; }
		public string? Password { get; set; }
	}
}
