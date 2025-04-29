using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Utilities;

namespace Advocate_Invoceing.DAL.Interface
{
    public interface IEmployeeLogRepo
    {
        Task<List<EmployeeLogDTO>> GetAllLogsAsync();
        Task<GenericResponse> CreateLogAsync(EmployeeLogDTO dto);
        Task<GenericResponse> UpdateLogAsync(EmployeeLogDTO dto);
        Task<GenericResponse> DeleteLogAsync(int logId);
        Task<int> GetTodaysWorkLogsAsync(int userId);
    }
}
