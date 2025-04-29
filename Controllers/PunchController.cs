using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.Models.Entity;
using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Advocate_Invoceing.Controllers
{
    public class PunchController : Controller
    {
        private readonly IEmployeePunchRepo _punchRepo;

        public PunchController(IEmployeePunchRepo punchRepo)
        {
            _punchRepo = punchRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            LoginResponseModel lr = new LoginResponseModel();
            lr = SessionHelper.GetObjectFromJson<LoginResponseModel>(HttpContext.Session, "loggedUser");
            if (lr == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            ViewBag.UserDetails = lr;
            int userId = lr.userId;
            var punches = await _punchRepo.GetPunchesByUserIdAsync(userId);
            return View(punches);
        }


        [HttpGet]
        public async Task<IActionResult> PunchHistory()
        {
            LoginResponseModel lr = new LoginResponseModel();
            lr = SessionHelper.GetObjectFromJson<LoginResponseModel>(HttpContext.Session, "loggedUser");
            if (lr == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            ViewBag.UserDetails = lr;
            int userId = lr.userId;

            // Fetch the punch history for the user
            var punchHistory = await _punchRepo.GetPunchHistoryByUserIdAsync(userId);

            return View(punchHistory);
        }

        [HttpPost]
        public async Task<IActionResult> Punch(string type)
        {
            LoginResponseModel lr = new LoginResponseModel();
            lr = SessionHelper.GetObjectFromJson<LoginResponseModel>(HttpContext.Session, "loggedUser");
            if (lr == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            ViewBag.UserDetails = lr;
            int userId = lr.userId;
            
            var punch = new EmployeePunch
            {
                UserId = userId,
                PunchType = type,
                PunchTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                CreatedOn = DateTime.Now,
                CreatedBy = userId
            };

            await _punchRepo.PunchAsync(punch);
            return RedirectToAction("Index", "Home");
        }
    }


   

}
