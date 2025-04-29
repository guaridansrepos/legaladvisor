using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Models.Entity;

namespace Advocate_Invoceing.DAL.Interface
{
    public interface IEmployeePunchRepo
    {
        Task PunchAsync(EmployeePunch punch);
        // Task<List<EmployeePunch>> GetPunchesByUserIdAsync(int userId);
        Task<List<EmployeeDashDTO>> GetPunchesByUserIdAsync(int userId);

        Task<List<EmployeePunch>> GetPunchHistoryByUserIdAsync(int userId);

    }
}
