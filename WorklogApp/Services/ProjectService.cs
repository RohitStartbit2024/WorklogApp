using Microsoft.EntityFrameworkCore;
using WorklogApp.Data;
using WorklogApp.Models;

namespace WorklogApp.Services
{
    public class ProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRole) // <- important
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Project>> GetProjectsByManagerAsync(int managerId)
        {
            // Assuming a UserProjects table links users to projects
            return await _context.UserProjects
                .Where(up => up.UserId == managerId)
                .Select(up => up.Project!)
                .Distinct()
                .ToListAsync();
        }

        public async Task AssignUsersToProject(int projectId, List<int> userIds)
        {
            var existingAssignments = _context.UserProjects.Where(up => up.ProjectId == projectId);
            _context.UserProjects.RemoveRange(existingAssignments);

            foreach (var userId in userIds)
            {
                _context.UserProjects.Add(new UserProject
                {
                    ProjectId = projectId,
                    UserId = userId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
