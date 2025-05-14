using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advocate_Invoceing.BAL.IService
{
	public interface IInvoiceService
	{
		//Task<GenericResponse> CreateInvoiceWithApprovalAsync(InvoiceDTO dto);
		Task<List<InvoiceDTO>> GetAllAsync();
		Task<InvoiceDTO> GetByIdAsync(int id);
		Task<GenericResponse> DeleteAsync(int id);
		Task<GenericResponse> CreateInvoiceWithApprovalAsync(InvoiceDTO dto);
		Task<List<InvoiceDTO>> GetInvoiceListWithClientAsync();
		Task<InvoiceDTO> GetInvoiceDetailsByIdAsync(int id);
		Task<GenericResponse> DeleteInvoiceAsyncs(int id);
	}
}
