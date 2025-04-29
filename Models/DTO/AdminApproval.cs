using System.ComponentModel.DataAnnotations;

namespace Advocate_Invoceing.Models.DTO
{
	public class AdminApproval
	{
		[Key]
		public int ApprovalId { get; set; }
		public string RelatedEntity { get; set; }
		public int EntityId { get; set; }
		public string Status { get; set; }
		public string Comment { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public int? UpdatedBy { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }
	}

}
