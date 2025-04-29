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
