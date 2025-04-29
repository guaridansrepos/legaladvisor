using Advocate_Invoceing.Models.DTO;
namespace Advocate_Invoceing.BAL.IService
{
	public interface IAdminApprovalRepo
	{
		Task<List<AdminApproval>> GetPendingApprovalsAsync();
		Task<AdminApproval> GetApprovalRequestByIdAsync(int approvalId);
		Task AddApprovalRequestAsync(AdminApproval approval);
		Task UpdateApprovalRequestAsync(AdminApproval approval);
	}
}
