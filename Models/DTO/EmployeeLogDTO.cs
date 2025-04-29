namespace Advocate_Invoceing.Models.DTO
{
    public class EmployeeLogDTO
    {
		public int WorkLogId { get; set; }
		public int UserId { get; set; }
		public string TaskTitle { get; set; }
		public string TaskDescription { get; set; }
		public decimal TimeSpent { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public int? UpdatedBy { get; set; }
	}
}
