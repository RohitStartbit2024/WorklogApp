using Microsoft.EntityFrameworkCore;
using WorklogApp.Data;
using WorklogApp.Models;
using Microsoft.AspNetCore.Identity;

namespace WorklogApp.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRole)
                .ToListAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUserAsync(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetEmployeesAsync()
        {
            return await _context.Users
                .Include(u => u.UserRole)
                .Where(u => u.UserRole != null && u.UserRole.Role == "Employee")
                .ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null)
                return;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
            }
            else
            {
                user.Password = existingUser.Password;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserRole>> GetRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }
    }
}
