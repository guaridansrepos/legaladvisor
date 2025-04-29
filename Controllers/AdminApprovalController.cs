using Advocate_Invoceing.BAL;
using Advocate_Invoceing.BAL.IService;
using Advocate_Invoceing.DAL;
using Advocate_Invoceing.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Advocate_Invoceing.Controllers
{
	public class AdminApprovalController : Controller
	{
		private readonly IAdminApprovalRepo _adminApprovalRepo;
		private readonly IClientService _clientRepo;

		public AdminApprovalController(IAdminApprovalRepo adminApprovalRepo, IClientService clientRepo)
		{
			_adminApprovalRepo = adminApprovalRepo;
			_clientRepo = clientRepo;
		}

		// Action to show the pending approval requests
		public async Task<IActionResult> PendingApprovals()
		{
			var pendingApprovals = await _adminApprovalRepo.GetPendingApprovalsAsync();
			return View(pendingApprovals);
		}

		// Action for user to submit something for approval (e.g., adding a client)
		public async Task<IActionResult> SubmitForApproval(int clientId)
		{
			var client = await _clientRepo.GetClientByIdAsync(clientId);
			if (client != null)
			{
				// Create a new approval request for the admin
				var approvalRequest = new AdminApproval
				{
					RelatedEntity = "Client",
					EntityId = clientId,
					Status = "Pending",
					CreatedOn = DateTime.Now,
					CreatedBy = 1,  // Assuming the logged-in user ID is 1
					IsActive = true
				};

				await _adminApprovalRepo.AddApprovalRequestAsync(approvalRequest);
				return RedirectToAction("Index", "Home");
			}

			return NotFound();
		}

		// Action for admin to approve/reject the request
		[HttpPost]
		public async Task<IActionResult> ApproveRejectApproval(int approvalId, string status, string comment)
		{
			var approvalRequest = await _adminApprovalRepo.GetApprovalRequestByIdAsync(approvalId);
			if (approvalRequest != null)
			{
				approvalRequest.Status = status;
				approvalRequest.Comment = comment;
				approvalRequest.UpdatedOn = DateTime.Now;
				approvalRequest.UpdatedBy = 1;  // Assuming the admin ID is 1

				await _adminApprovalRepo.UpdateApprovalRequestAsync(approvalRequest);
				return RedirectToAction("PendingApprovals");
			}

			return NotFound();
		}
	}

}
