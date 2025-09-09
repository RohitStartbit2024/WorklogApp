using Microsoft.EntityFrameworkCore;
using WorklogApp.Data;
using WorklogApp.Models;

namespace WorklogApp.Services
{
    public class WorklogService
    {
        private readonly AppDbContext _context;

        public WorklogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Worklog>> GetWorklogsByUserAsync(int userId)
        {
            return await _context.Worklogs
                .Include(w => w.Project)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.Date)
                .ToListAsync();
        }

        public async Task AddWorklogAsync(Worklog worklog)
        {
            _context.Worklogs.Add(worklog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWorklogAsync(Worklog worklog)
        {
            _context.Worklogs.Update(worklog);
            await _context.SaveChangesAsync();
        }

        public async Task<Worklog?> GetWorklogAsync(int id)
        {
            return await _context.Worklogs
                .Include(w => w.Project)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Project>> GetUserProjectsAsync(int userId)
        {
            return await _context.UserProjects
                .Where(up => up.UserId == userId)
                .Select(up => up.Project!)
                .ToListAsync();
        }

        public async Task<List<Worklog>> GetWorklogsByProjectAsync(int projectId)
        {
            return await _context.Worklogs
                .Include(w => w.User)
                .Where(w => w.ProjectId == projectId)
                .OrderByDescending(w => w.Date)
                .ToListAsync();
        }

        public async Task<List<Worklog>> GetWorklogsByEmployeeAsync(int userId)
        {
            return await _context.Worklogs
                .Include(w => w.Project)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.Date)
                .ToListAsync();
        }

        public async Task ApproveWorklogAsync(int worklogId)
        {
            var w = await _context.Worklogs.FindAsync(worklogId);
            if (w != null)
            {
                w.Status = WorklogStatus.Approved;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RejectWorklogAsync(int worklogId)
        {
            var w = await _context.Worklogs.FindAsync(worklogId);
            if (w != null)
            {
                w.Status = WorklogStatus.Rejected;
                await _context.SaveChangesAsync();
            }
        }

    }
}
