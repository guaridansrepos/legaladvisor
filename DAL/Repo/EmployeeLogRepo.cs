using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.Models.DTO;
using Advocate_Invoceing.Models.Entity;
using Advocate_Invoceing.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate_Invoceing.DAL.Repo
{
	public class EmployeeWorkLogRepo : IEmployeeLogRepo
	{
		private readonly MyDbContext _context;

		public EmployeeWorkLogRepo(MyDbContext context)
		{
			_context = context;
		}

		public async Task<List<EmployeeLogDTO>> GetAllLogsAsync()
		{
			return await _context.EmployeeLogs
				.Select(log => new EmployeeLogDTO
				{
					WorkLogId = log.WorkLogId,
					UserId = log.UserId,
					TaskTitle = log.TaskTitle,
					TaskDescription = log.TaskDescription,
					TimeSpent = log.TimeSpent,
					IsDeleted = log.IsDeleted,
					IsActive = log.IsActive,
					CreatedOn = log.CreatedOn,
					CreatedBy = log.CreatedBy,
					UpdatedOn = log.UpdatedOn,
					UpdatedBy = log.UpdatedBy
				})
				.OrderByDescending(log => log.CreatedOn)
				.ToListAsync();
		}

		public async Task<GenericResponse> CreateLogAsync(EmployeeLogDTO dto)
		{
			var log = new EmployeeLog
			{
				UserId = dto.UserId,
				TaskTitle = dto.TaskTitle,
				TaskDescription = dto.TaskDescription,
				TimeSpent = dto.TimeSpent,
				IsDeleted = false,
				IsActive = true,
				CreatedOn = DateTime.Now,
				CreatedBy = dto.CreatedBy,
			};

			_context.EmployeeLogs.Add(log);
			await _context.SaveChangesAsync();

			return new GenericResponse
			{
				message = "Employee work log created successfully",
				statuCode = 1,
				currentId = log.WorkLogId
			};
		}

		public async Task<GenericResponse> UpdateLogAsync(EmployeeLogDTO dto)
		{
			var log = await _context.EmployeeLogs.FindAsync(dto.WorkLogId);
			if (log == null)
			{
				return new GenericResponse
				{
					message = "Work log not found",
					statuCode = 0,
					currentId = 0
				};
			}

			log.TaskTitle = dto.TaskTitle;
			log.TaskDescription = dto.TaskDescription;
			log.TimeSpent = dto.TimeSpent;
			log.UpdatedOn = DateTime.Now;
			log.UpdatedBy = dto.UpdatedBy;

			_context.EmployeeLogs.Update(log);
			await _context.SaveChangesAsync();

			return new GenericResponse
			{
				message = "Employee work log updated successfully",
				statuCode = 1,
				currentId = log.WorkLogId
			};
		}

		public async Task<GenericResponse> DeleteLogAsync(int workLogId)
		{
			var log = await _context.EmployeeLogs.FindAsync(workLogId);
			if (log == null)
			{
				return new GenericResponse
				{
					message = "Work log not found",
					statuCode = 0,
					currentId = 0
				};
			}

			// Soft Delete (if needed)
			log.IsDeleted = true;
			_context.EmployeeLogs.Update(log);
			await _context.SaveChangesAsync();

			return new GenericResponse
			{
				message = "Employee work log deleted successfully",
				statuCode = 1,
				currentId = log.WorkLogId
			};
		}

		public async Task<int> GetTodaysWorkLogsAsync(int userId)
		{
			var today = DateTime.Today;
			return await _context.EmployeeLogs
				.CountAsync(w => w.UserId == userId && w.CreatedOn.Date == today && w.IsDeleted == false);
		}
	}
}
