using System.ComponentModel.DataAnnotations;

namespace Advocate_Invoceing.Models.Entity
{
	public class EmployeeLog
	{
		[Key]
		public int WorkLogId { get; set; }
		public int UserId { get; set; }
		public string TaskTitle { get; set; }
		public string TaskDescription { get; set; }
		public decimal TimeSpent { get; set; } // Assuming TimeSpent is stored as decimal
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; } // Nullable because updated might not be done
		public int? UpdatedBy { get; set; }      // Nullable for same reason
	}
}
