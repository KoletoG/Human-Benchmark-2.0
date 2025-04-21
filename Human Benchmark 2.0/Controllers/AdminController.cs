using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Human_Benchmark_2._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _logger = logger;
            _context = context;
            _memoryCache = memoryCache;
        }
        // Add searching by name
        // Cache user and usercount, maybe dict for cached users
        [Authorize]
        public async Task<IActionResult> AdminMain(int page = 1)
        {
            string currentName = this.User.Identity?.Name ?? "";
            if (currentName != Constants.Constants.adminName)
            {
                return View("Index");
            }
            int countUsersByPage = 2;
            if(!_memoryCache.TryGetValue("count", out int count))
            {
                count = await _context.Users.CountAsync();
                _memoryCache.Set("count", count);
            }
            int countUsers = await _context.Users.CountAsync();
            int pageAll = (int)Math.Ceiling((double)count / countUsersByPage);
            if (count != countUsers)
            {
                for (int i = 1; i <= pageAll; i++)
                {
                    try
                    {
                        _memoryCache.Remove(i);
                    }
                    catch (KeyNotFoundException)
                    {

                    }
                }
                _memoryCache.Set("count", countUsers); 
                pageAll = (int)Math.Ceiling((double)countUsers / countUsersByPage);
            }
            if (page < 1) page = 1;
            if (page > pageAll) page = pageAll;
            if(!_memoryCache.TryGetValue($"User_{currentName}",out UserDataModel user))
            {
                user = await _context.GetUserByNameAsync(currentName);
                _memoryCache.Set($"User_{currentName}",user, TimeSpan.FromMinutes(3));
            }
            if (!_memoryCache.TryGetValue(page,out List<UserDataModel> users))
            {
                users = await _context.Users.Skip((page - 1) * countUsersByPage)
                                            .Take(countUsersByPage)
                                            .ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                                                                     .SetSlidingExpiration(TimeSpan.FromMinutes(3));
                _memoryCache.Set(page, users,cacheEntryOptions);
            }
            bool firstPage = page == 1;
            bool finalPage = page == pageAll;
            return View(new AdminMainViewModel(user, users, page, finalPage, firstPage));
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDelete(string idOfUser, int page)
        {
            if (this.User.Identity.Name != "Admin")
            {
                return View("Index");
            }
            var userToDelete = await _context.Users.SingleOrDefaultAsync(x => x.Id == idOfUser);
            if (userToDelete != default)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
            _memoryCache.Remove(page);
            return RedirectToAction("AdminMain");
        }
    }
}
