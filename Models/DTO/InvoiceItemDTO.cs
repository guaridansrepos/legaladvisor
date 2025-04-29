namespace Advocate_Invoceing.Models.DTO
{
    public class InvoiceItemDTO
    {
        public int ItemId { get; set; }
        public int InvoiceId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
