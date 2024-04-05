
using System.Text.Json.Serialization;

namespace Base_Library.Entities
{
	public class BaseEntityWithRel
	{
		public int Id { get; set; }
		public string? Name { get; set; }

		//one to many
		[JsonIgnore]
		public List<Employee>? Employees { get; set; } = [];

	}
}
