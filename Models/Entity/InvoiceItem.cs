using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Advocate_Invoceing.Models.Entity
{
    public class InvoiceItem
    {
        [Key]
        public int ItemId { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalAmount { get; set; } // (Quantity * Rate) - handled by database computed column

        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        
    }
}
