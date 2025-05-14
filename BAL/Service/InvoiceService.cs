// File: BAL/Service/InvoiceService.cs
using Advocate_Invoceing.BAL.IService;
using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.DAL.Repo;
using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Utilities;
using System.Threading.Tasks;

namespace Advocate_Invoceing.BAL.Service
{
	public class InvoiceService : IInvoiceService
	{
		private readonly IInvoiceRepo _invoiceRepo;

		public InvoiceService(IInvoiceRepo invoiceRepo)
		{
			_invoiceRepo = invoiceRepo;
		}

		public Task<GenericResponse> CreateInvoiceWithApprovalAsync(InvoiceDTO dto)
		{
			return _invoiceRepo.CreateInvoiceWithApprovalAsync(dto);
		}

		public Task<List<InvoiceDTO>> GetAllAsync() => _invoiceRepo.GetAllInvoicesAsync();

		public Task<InvoiceDTO> GetByIdAsync(int id) => _invoiceRepo.GetInvoiceByIdAsync(id);

		public Task<GenericResponse> DeleteAsync(int id) => _invoiceRepo.DeleteInvoiceAsync(id);
		 

		public Task<List<InvoiceDTO>> GetInvoiceListWithClientAsync() => _invoiceRepo.GetInvoiceListWithClientAsync();

		public Task<InvoiceDTO> GetInvoiceDetailsByIdAsync(int id) => _invoiceRepo.GetInvoiceDetailsByIdAsync(id);

		public Task<GenericResponse> DeleteInvoiceAsyncs(int id) => _invoiceRepo.DeleteInvoiceAsyncs(id);
	}
}
