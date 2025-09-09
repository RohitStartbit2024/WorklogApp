using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using WorklogApp.Data;
using WorklogApp.Dto;
using WorklogApp.Models;

namespace WorklogApp.Services;

public class CurrentUserService
{
    private readonly ProtectedLocalStorage _storage;
    private readonly AppDbContext _context;

    public CurrentUserDto? CurrentUser { get; private set; }

    public CurrentUserService(ProtectedLocalStorage storage, AppDbContext context)
    {
        _storage = storage;
        _context = context;
    }

    public async Task LoadUserAsync()
    {
        var result = await _storage.GetAsync<CurrentUserDto>("currentUser");
        if (result.Success)
            CurrentUser = result.Value;
    }

    public async Task SetUserAsync(User user)
    {
        CurrentUser = new CurrentUserDto
        {
            Id = user.Id,
            EmployeeId = user.EmployeeId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserRoleId = user.UserRoleId,
            RoleName = user.UserRole?.Role ?? ""
        };

        await _storage.SetAsync("currentUser", CurrentUser);
    }

    public async Task LogoutAsync()
    {
        CurrentUser = null;
        await _storage.DeleteAsync("currentUser");
    }
}
