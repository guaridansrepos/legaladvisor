namespace Advocate_Invoceing.Models.DTO
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Discount { get; set; }
        public string? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public string? DSCFilePath { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? GSTPercentage { get; set; }
        public decimal? CGSTAmount { get; set; }
        public decimal? SGSTAmount { get; set; }

        public List<InvoiceItemDTO> InvoiceItems { get; set; }
    }
}
