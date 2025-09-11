using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OfficeOpenXml; 
using WorklogApp.Components;
using WorklogApp.Data;
using WorklogApp.Models;
using WorklogApp.Services;

namespace WorklogApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Startbit");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ProjectService>();
            builder.Services.AddScoped<WorklogService>();
            builder.Services.AddScoped<CurrentUserService>();
            builder.Services.AddScoped<ProtectedLocalStorage>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapGet("/export/worklogs", async (HttpContext context, IMemoryCache cache, string key) =>
            {
                if (!cache.TryGetValue(key, out List<Worklog>? worklogs) || worklogs == null || !worklogs.Any())
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("No worklogs found to export.");
                    return;
                }

                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Worklogs");

                // Headers
                ws.Cells[1, 1].Value = "S.No";
                ws.Cells[1, 2].Value = "Employee Id";
                ws.Cells[1, 3].Value = "Employee Name";
                ws.Cells[1, 4].Value = "Project Id";
                ws.Cells[1, 5].Value = "Project";
                ws.Cells[1, 6].Value = "Date";
                ws.Cells[1, 7].Value = "Worklog Description";
                ws.Cells[1, 8].Value = "Online";
                ws.Cells[1, 9].Value = "Offline";
                ws.Cells[1, 10].Value = "Other";
                ws.Cells[1, 11].Value = "Total";
                ws.Cells[1, 12].Value = "Status";

                int row = 2;
                int index = 1;
                foreach (var w in worklogs)
                {
                    ws.Cells[row, 1].Value = index++;
                    ws.Cells[row, 2].Value = w.User?.EmployeeId;
                    ws.Cells[row, 3].Value = $"{w.User?.FirstName} {w.User?.LastName}";
                    ws.Cells[row, 4].Value = w.Project?.ProjectId;
                    ws.Cells[row, 5].Value = w.Project?.Name;
                    ws.Cells[row, 6].Value = w.Date.ToShortDateString();
                    ws.Cells[row, 7].Value = w.Description;
                    ws.Cells[row, 8].Value = w.OnlineHours;
                    ws.Cells[row, 9].Value = w.OfflineHours;
                    ws.Cells[row, 10].Value = w.OtherHours;
                    ws.Cells[row, 11].Value = w.OnlineHours + w.OfflineHours + w.OtherHours;
                    ws.Cells[row, 12].Value = w.Status.ToString();
                    row++;
                }

                var bytes = await package.GetAsByteArrayAsync();

                context.Response.Headers.ContentDisposition = "attachment; filename=worklogs.xlsx";
                context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                await context.Response.Body.WriteAsync(bytes);
            });

            app.Run();
        }
    }
}
