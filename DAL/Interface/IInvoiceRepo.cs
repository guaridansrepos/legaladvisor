using Advocate_Invoceing.Utilities;
using Advocate_Invoceing.Models.DTO;

namespace Advocate_Invoceing.DAL.Interface
{
    public interface IInvoiceRepo
    {
        Task<List<InvoiceDTO>> GetAllInvoicesAsync();
        Task<InvoiceDTO> GetInvoiceByIdAsync(int id);
        Task<GenericResponse> CreateInvoiceAsync(InvoiceDTO invoice);
        Task<GenericResponse> UpdateInvoiceAsync(InvoiceDTO invoice);
         Task<int> GetTotalInvoicesAsync();
        Task<GenericResponse> DeleteInvoiceAsync(int id);
        Task<int> GetPendingBillsAsync();
    }

}
