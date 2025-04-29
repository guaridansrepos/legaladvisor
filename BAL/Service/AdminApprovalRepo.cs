using Advocate_Invoceing.BAL.IService;
using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Advocate_Invoceing.BAL.Service
{
	public class AdminApprovalRepo : IAdminApprovalRepo
	{
		private readonly MyDbContext _context;

		public AdminApprovalRepo(MyDbContext context)
		{
			_context = context;
		}

		public async Task<List<AdminApproval>> GetPendingApprovalsAsync()
		{
			return await _context.AdminApprovals
								 .Where(a => a.Status == "Pending")
								 .ToListAsync();
		}

		public async Task<AdminApproval> GetApprovalRequestByIdAsync(int approvalId)
		{
			return await _context.AdminApprovals
								 .FirstOrDefaultAsync(a => a.ApprovalId == approvalId);
		}

		public async Task AddApprovalRequestAsync(AdminApproval approval)
		{
			_context.AdminApprovals.Add(approval);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateApprovalRequestAsync(AdminApproval approval)
		{
			_context.AdminApprovals.Update(approval);
			await _context.SaveChangesAsync();
		}
	}
}
