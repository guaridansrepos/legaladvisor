using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.DAL.Repo;
using Microsoft.AspNetCore.Mvc;
using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.Utilities;

namespace Advocate_Invoceing.Controllers
{
    public class EmployeeLogController : Controller
    {
        private readonly IEmployeeLogRepo _logRepo;

        public EmployeeLogController(IEmployeeLogRepo logRepo)
        {
            _logRepo = logRepo;
        }
         
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var logs = await _logRepo.GetAllLogsAsync();
            return View(logs);
        }

        
        [HttpGet]
        public IActionResult CreateEdit(int? id)
        {
            if (id == null)
            {
                return View(new EmployeeLogDTO());
            }
             
            return View(new EmployeeLogDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(EmployeeLogDTO dto)
        {
            GenericResponse response;
            if (dto.UserId == 0)
            {
                response = await _logRepo.CreateLogAsync(dto);
            }
            else
            {
                response = await _logRepo.UpdateLogAsync(dto);
            }

            if (response.statuCode == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                 
                return View(dto);
            }
        }

       
        [HttpPost]
        public async Task<IActionResult> Delete(int logId)
        {
            var response = await _logRepo.DeleteLogAsync(logId);
            if (response.statuCode == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
               
                return RedirectToAction("Index");
            }
        }
    }
}
