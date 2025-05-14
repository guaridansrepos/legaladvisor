using Advocate_Invoceing.BAL.IService;
using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.DAL.Repo;
using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Advocate_Invoceing.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IInvoiceService _invoiceService;


        public InvoiceController(IInvoiceRepo invoiceRepo, IInvoiceService invoiceService)
        {
            _invoiceRepo = invoiceRepo;
            _invoiceService = invoiceService;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceRepo.GetAllInvoicesAsync();
            return View(invoices);
        }

        public async Task<IActionResult> CreateEdit(int? id)
        {
            if (id == null)
                return View(new InvoiceDTO());

            var invoice = await _invoiceRepo.GetInvoiceByIdAsync(id.Value);
            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(InvoiceDTO dto)
        {
            if (ModelState.IsValid)
            {
                GenericResponse response;
                if (dto.InvoiceId == 0)
                {
                    response = await _invoiceRepo.CreateInvoiceAsync(dto);
                }
                else
                {
                    response = await _invoiceRepo.UpdateInvoiceAsync(dto);
                }

                if (response.statuCode == 1)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", response.message);
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _invoiceRepo.DeleteInvoiceAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewInvoice(int id)
        {
            var invoice = await _invoiceRepo.GetInvoiceByIdAsync(id);
            if (invoice == null) return NotFound();
            return View(invoice);
        }
         

		public async Task<IActionResult> Indexx()
		{
			var data = await _invoiceService.GetAllAsync();
			return View(data);
		}

        public async Task<IActionResult> CreateEditt(int? id)
        {
            if (!id.HasValue)
            {
                 return NotFound();  
            }

            var invoice = await _invoiceService.GetByIdAsync(id.Value);
            if (invoice == null)
            {
                return View(new InvoiceDTO());
            }
            return View(invoice);
        }



        //public async Task<IActionResult> CreateEditt(int? id)
        //{
        //    if (id == null)
        //        return View(new InvoiceDTO());

        //    var dto = await _invoiceService.GetByIdAsync(id.Value);
        //    return View(dto);
        //}

        [HttpPost]
		public async Task<IActionResult> CreateEditt(InvoiceDTO dto)
		{
			if (ModelState.IsValid)
			{
				var res = await _invoiceService.CreateInvoiceWithApprovalAsync(dto);
				if (res.statuCode == 1)
					return RedirectToAction("Index");
				ModelState.AddModelError("", res.message);
			}
			return View(dto);
		}

		public async Task<IActionResult> Deleter(int id)
		{
			await _invoiceService.DeleteAsync(id);
			return RedirectToAction("Index");
		}
	}




}

