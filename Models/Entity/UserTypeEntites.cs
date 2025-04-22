using System.ComponentModel.DataAnnotations;

namespace Advocate_Invoceing.Models.Entity
{
    public class UserTypeEntites
    {
        [Key]
        public int UserTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UserTypeName { get; set; }

        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
