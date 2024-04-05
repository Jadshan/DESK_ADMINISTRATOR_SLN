namespace Base_Library.Entities
{
	public class Employee : BaseEntity
	{

		public string? CivilId { get; set; }
		public string? FileNumber { get; set; }
		public string? FileName { get; set; }
		public string? JobName { get; set; }
		public string? Address { get; set; }
		public string? TelNo { get; set; }
		public string? Photo { get; set; }
		public string? Other { get; set; }

		//Many to one
		public GeneralDep? GeneralDep { get; set; }
		public int GeneralDepId { get; set; }

		public Departtment? Departtment { get; set; }
		public int DeparttmentId { get; set; }

		public Branch? Branch { get; set; }
		public int BranchId { get; set; }

		public Town? Town { get; set; }
		public int TownId { get; set; }
	}
}
