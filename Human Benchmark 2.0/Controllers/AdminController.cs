using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Human_Benchmark_2._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> AdminMain()
        {
            if (this.User.Identity.Name != "Admin")
            {
                return View("Index");
            }
            var user = await _context.GetUserByNameAsync(this.User.Identity.Name);
            var users = await _context.Users.ToListAsync();
            return View(new AdminMainViewModel(user,users));
        }
    }
}
