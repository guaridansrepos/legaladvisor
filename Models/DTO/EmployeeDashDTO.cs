namespace Advocate_Invoceing.Models.DTO
{
	public class EmployeeDashDTO
	{
		public int PunchId { get; set; }
		public int UserId { get; set; }
		public string PunchType { get; set; } // "IN" or "OUT"
		public DateTime PunchTime { get; set; }
	}
}
