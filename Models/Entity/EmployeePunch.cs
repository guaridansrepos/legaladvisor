using System.ComponentModel.DataAnnotations;

namespace Advocate_Invoceing.Models.Entity
{
    public class EmployeePunch
    {
        [Key]
        public int PunchId { get; set; }
        public int UserId { get; set; }
        public string PunchType { get; set; } // "IN" or "OUT"
        public DateTime PunchTime { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
