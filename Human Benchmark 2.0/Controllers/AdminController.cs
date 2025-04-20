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
        // Add searching by name
        [Authorize]
        public async Task<IActionResult> AdminMain(int page=1)
        {
            if (this.User.Identity.Name != Constants.Constants.adminName)
            {
                return View("Index");
            }
            int countUsersByPage = 5;
            int count = await _context.Users.CountAsync();
            int pageAll = (int)Math.Ceiling((double)count / countUsersByPage);
            if (page < 1) page = 1;
            if (page > pageAll) page = pageAll;
            var user = await _context.GetUserByNameAsync(this.User.Identity.Name);
            var users = await _context.Users.Skip((page-1)* countUsersByPage).Take(countUsersByPage).ToListAsync();
            bool firstPage = page == 1;
            bool finalPage = page == pageAll;
            return View(new AdminMainViewModel(user,users,page, finalPage, firstPage));
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDelete(string idOfUser)
        {
            if (this.User.Identity.Name != "Admin")
            {
                return View("Index");
            }
            var userToDelete = await _context.Users.SingleAsync(x=>x.Id== idOfUser);
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminMain");
        }
    }
}
