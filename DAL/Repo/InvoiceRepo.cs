using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Models.Entity;
using Advocate_Invoceing.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate_Invoceing.DAL.Repo
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly MyDbContext _context;

        public InvoiceRepo(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDTO>> GetAllInvoicesAsync()
        {
            var invoices = await (from invoice in _context.Invoices
                                  join client in _context.Clients on invoice.ClientId equals client.ClientId
                                  where invoice.IsDeleted != true
                                  select new InvoiceDTO
                                  {
                                      InvoiceId = invoice.InvoiceId,
                                      ClientName = client.FullName,
                                      Amount = invoice.Amount,
                                      DueDate = invoice.DueDate,
                                      Status = invoice.Status,
                                      InvoiceNumber = invoice.InvoiceNumber
                                  })
                                  .OrderByDescending(x => x.InvoiceId)
                                  .ToListAsync();

            return invoices;
        }


        public async Task<InvoiceDTO> GetInvoiceByIdAsync(int id)
        {
            var invoice = await (from inv in _context.Invoices
                                 join client in _context.Clients on inv.ClientId equals client.ClientId
                                 where inv.InvoiceId == id && inv.IsDeleted != true
                                 select new InvoiceDTO
                                 {
                                     InvoiceId = inv.InvoiceId,
                                     ClientName = client.FullName,
                                     Amount = inv.Amount,
                                     DueDate = inv.DueDate,
                                     Status = inv.Status,
                                     InvoiceNumber = inv.InvoiceNumber
                                 }).FirstOrDefaultAsync();

            return invoice;
        }


        public async Task<GenericResponse> CreateInvoiceAsync(InvoiceDTO dto)
        {
            Invoice inv = new Invoice
            {
                ClientId = dto.ClientId,
                Amount = dto.Amount,
                DueDate = dto.DueDate,
                Status = dto.Status,
                InvoiceNumber = dto.InvoiceNumber,
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            _context.Invoices.Add(inv);
            await _context.SaveChangesAsync();

            return new GenericResponse
            {
                message = "Invoice Created Successfully",
                statuCode = 1,
                currentId = inv.InvoiceId
            };
        }

        public async Task<GenericResponse> UpdateInvoiceAsync(InvoiceDTO dto)
        {
            var inv = await _context.Invoices.FindAsync(dto.InvoiceId);
            if (inv == null)
            {
                return new GenericResponse
                {
                    message = "Invoice Not Found",
                    statuCode = 0,
                    currentId = 0
                };
            }

            inv.Amount = dto.Amount;
            inv.DueDate = dto.DueDate;
            inv.Status = dto.Status;
            inv.InvoiceNumber = dto.InvoiceNumber;
            inv.UpdatedOn = DateTime.Now;

            _context.Invoices.Update(inv);
            await _context.SaveChangesAsync();

            return new GenericResponse
            {
                message = "Invoice Updated Successfully",
                statuCode = 1,
                currentId = inv.InvoiceId
            };
        }

        public async Task<GenericResponse> DeleteInvoiceAsync(int id)
        {
            var inv = await _context.Invoices.FindAsync(id);
            if (inv == null)
            {
                return new GenericResponse
                {
                    message = "Invoice Not Found",
                    statuCode = 0,
                    currentId = 0
                };
            }

            inv.IsDeleted = true;
            _context.Invoices.Update(inv);
            await _context.SaveChangesAsync();

            return new GenericResponse
            {
                message = "Invoice Deleted Successfully",
                statuCode = 1,
                currentId = inv.InvoiceId
            };
        }

		public async Task<GenericResponse> CreateInvoiceWithApprovalAsync(InvoiceDTO dto)
		{
			var invoice = new Invoice
			{
				ClientId = dto.ClientId,
				Amount = dto.Amount,
				Tax = dto.Tax,
				Discount = dto.Discount,
				SubTotal = dto.SubTotal,
				GSTPercentage = dto.GSTPercentage,
				CGSTAmount = dto.CGSTAmount,
				SGSTAmount = dto.SGSTAmount,
				InvoiceNumber = dto.InvoiceNumber,
				InvoiceDate = dto.InvoiceDate,
				DueDate = dto.DueDate,
				DSCFilePath = dto.DSCFilePath,
				CreatedOn = DateTime.Now,
				CreatedBy = dto.CreatedBy,
				Status = "PendingApproval",
				IsActive = false,
				IsDeleted = false
			};

			_context.Invoices.Add(invoice);
			await _context.SaveChangesAsync();

			var approval = new AdminApproval
			{
				RelatedEntity = "Invoice",
				EntityId = invoice.InvoiceId,
				Status = "Pending",
				Comment = "",
				CreatedOn = DateTime.Now,
				CreatedBy = (int)dto.CreatedBy,
				IsActive = true,
				IsDeleted = false
			};

			_context.AdminApprovals.Add(approval);
			await _context.SaveChangesAsync();

			return new GenericResponse
			{
				message = "Invoice submitted for approval",
				statuCode = 1,
				currentId = invoice.InvoiceId
			};
		}

    
		public async Task<List<InvoiceDTO>> GetInvoiceListWithClientAsync()
		{
			var query = from inv in _context.Invoices
						join cli in _context.Clients on inv.ClientId equals cli.ClientId
						where inv.IsDeleted == false
						select new InvoiceDTO
						{
							InvoiceId = inv.InvoiceId,
							ClientId = inv.ClientId,
							ClientName = cli.FullName,
							Amount = inv.Amount,
							Status = inv.Status,
							InvoiceNumber = inv.InvoiceNumber,
							DueDate = inv.DueDate
						};
			return await query.ToListAsync();
		}

		public async Task<InvoiceDTO> GetInvoiceDetailsByIdAsync(int id)
		{
			var query = from inv in _context.Invoices
						join cli in _context.Clients on inv.ClientId equals cli.ClientId
						where inv.InvoiceId == id && inv.IsDeleted == false
						select new InvoiceDTO
						{
							InvoiceId = inv.InvoiceId,
							ClientId = inv.ClientId,
							ClientName = cli.FullName,
							Amount = inv.Amount,
							Tax = inv.Tax,
							Discount = inv.Discount,
							SubTotal = inv.SubTotal,
							GSTPercentage = inv.GSTPercentage,
							CGSTAmount = inv.CGSTAmount,
							SGSTAmount = inv.SGSTAmount,
							InvoiceNumber = inv.InvoiceNumber,
							DueDate = inv.DueDate,
							DSCFilePath = inv.DSCFilePath,
							Status = inv.Status,
							CreatedOn = inv.CreatedOn,
							CreatedBy = inv.CreatedBy
						};
			return await query.FirstOrDefaultAsync();
		}

		public async Task<GenericResponse> DeleteInvoiceAsyncs(int id)
		{
			var inv = await _context.Invoices.FindAsync(id);
			if (inv == null)
			{
				return new GenericResponse { message = "Invoice not found", statuCode = 0 };
			}

			inv.IsDeleted = true;
			_context.Invoices.Update(inv);
			await _context.SaveChangesAsync();

			return new GenericResponse { message = "Invoice deleted", statuCode = 1, currentId = inv.InvoiceId };
		}



		public async Task<int> GetTotalInvoicesAsync()
        {
            return await _context.Invoices.CountAsync();
        }

        public async Task<int> GetPendingBillsAsync()
        {
            return await _context.Invoices.CountAsync(i => i.Status == "Pending" && i.IsDeleted == false);
        }


	 

	}
}
