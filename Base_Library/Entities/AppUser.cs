
namespace Base_Library.Entities
{
	internal class AppUser : BaseEntity
	{
		public string? UserName { get; set; }
		public string? UserEmail { get; set; }
		public string? Password { get; set; }
	}
}
