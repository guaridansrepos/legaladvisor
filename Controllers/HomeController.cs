using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.Models;
using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Advocate_Invoceing.DAL;
using Advocate_Invoceing.BAL;


namespace Advocate_Invoceing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeePunchRepo _punchRepo;
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IClientService _clientRepo;
        private readonly IEmployeeLogRepo _workLogRepo;

        // Constructor to inject repositories
        public HomeController(ILogger<HomeController> logger,
                              IEmployeePunchRepo punchRepo,
                              IInvoiceRepo invoiceRepo,
                              IClientService clientRepo,
                              IEmployeeLogRepo workLogRepo)
        {
            _logger = logger;
            _punchRepo = punchRepo;
            _invoiceRepo = invoiceRepo;
            _clientRepo = clientRepo;
            _workLogRepo = workLogRepo;
        }

        public async Task<IActionResult> Index()
        {
            // Get logged-in user details from session
            LoginResponseModel lr = SessionHelper.GetObjectFromJson<LoginResponseModel>(HttpContext.Session, "loggedUser");
            if (lr == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            ViewBag.UserDetails = lr;
            int userId = lr.userId;

            // Fetching dashboard data
            var totalInvoices = await _invoiceRepo.GetTotalInvoicesAsync();
            var pendingBills = await _invoiceRepo.GetPendingBillsAsync();
            var activeClients = await _clientRepo.GetActiveClientsAsync();
            var todaysWorkLogs = await _workLogRepo.GetTodaysWorkLogsAsync(userId);

            // Preparing the data for the dashboard
            ViewBag.TotalInvoices = totalInvoices;
            ViewBag.PendingBills = pendingBills;
            ViewBag.ActiveClients = activeClients;
            ViewBag.TodaysWorkLogs = todaysWorkLogs;

            // Get employee punches (optional for logging in/out functionality)
            var punches = await _punchRepo.GetPunchesByUserIdAsync(userId);

            return View(punches);
        }
    


        public IActionResult TableCopy()
		{
			return View();
		}
	}
}
