using Microsoft.EntityFrameworkCore;
using WorklogApp.Data;
using WorklogApp.Models;

namespace WorklogApp.Services
{
    public class ProjectService
    {
        private readonly AppDbContext _context;
        private readonly CurrentUserService _currentUser;

        public ProjectService(AppDbContext context, CurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRole)
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            if (_currentUser.CurrentUser != null)
                project.ManagerId = _currentUser.CurrentUser.Id;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
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

        // 🔹 This is the correct way: filter by ManagerId, not UserProjects
        public async Task<List<Project>> GetMyProjectsAsync()
        {
            if (_currentUser.CurrentUser == null)
                return new List<Project>();

            var managerId = _currentUser.CurrentUser.Id;

            return await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .Where(p => p.ManagerId == managerId)
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
