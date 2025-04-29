using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Advocate_Invoceing.DAL.Repo
{
    public class EmployeePunchRepo : IEmployeePunchRepo
    {
        private readonly MyDbContext _context;

        public EmployeePunchRepo(MyDbContext context)
        {
            _context = context;
        }

        public async Task PunchAsync(EmployeePunch punch)
        {
            _context.EmployeePunches.Add(punch);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<EmployeePunch>> GetPunchesByUserIdAsync(int userId)
        //{
        //    return await _context.EmployeePunches
        //        .Where(x => x.UserId == userId && x.IsDeleted != true)
        //        .OrderByDescending(x => x.PunchTime)
        //        .ToListAsync();
        //}

		public async Task<List<EmployeeDashDTO>> GetPunchesByUserIdAsync(int userId)
		{
			return await _context.EmployeePunches
				.Where(x => x.UserId == userId && x.IsDeleted != true)
				.OrderByDescending(x => x.PunchTime)
				.Select(x => new EmployeeDashDTO
				{
					PunchId = x.PunchId,
					UserId = x.UserId,
					PunchType = x.PunchType,
					PunchTime = x.PunchTime
				})
				.ToListAsync();
		}

        public async Task<List<EmployeePunch>> GetPunchHistoryByUserIdAsync(int userId)
        {
            return await _context.EmployeePunches
                                 .Where(x => x.UserId == userId && x.IsDeleted != true)
                                 .OrderByDescending(x => x.PunchTime)
                                 .ToListAsync();
        }

    }
}
