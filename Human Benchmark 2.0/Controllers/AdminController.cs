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
        // Add comments
        [Authorize]
        public async Task<IActionResult> AdminMain(int page = 1)
        {
            string currentName = this.User.Identity?.Name ?? "";
            if (currentName != Constants.Constants.adminName)
            {
                return View("Index");
            }
            int countUsersByPage = 2; // Sets how many users should be shown per page
            int countUsers = await _context.Users.CountAsync();
            if (!_memoryCache.TryGetValue("count", out int cachedCount) || cachedCount != countUsers) // Checks if a user has registered or deleted his profile and therefore changing the user count
            {
                if (_memoryCache.TryGetValue("pageCount", out int pageCachedAll))
                {
                    for (int i = 1; i <= pageCachedAll; i++) // Removes every page that has been created with previous user count
                    {
                        _memoryCache.Remove($"Page:{i}");
                    }
                    _memoryCache.Remove("pageCount");
                }
                else
                {
                    if (cachedCount != default)
                    {
                        for (int i = 1; i <= Math.Ceiling((double)cachedCount / countUsersByPage); i++) // Removes every page that has been created with previous user count
                        {
                            _memoryCache.Remove($"Page:{i}");
                        }
                    }

                }
            }
            if(cachedCount != countUsers)
            {
               _memoryCache.Set("count", countUsers);
            }
            if (!_memoryCache.TryGetValue("pageCount", out int pageAll))
            {
                pageAll = (int)Math.Ceiling((double)countUsers / countUsersByPage); // Calculates the maximum of pages with current user count
                _memoryCache.Set("pageCount", pageAll);
            }
            if (page < 1) page = 1; // These 2 lines handle exceptions if user has manually entered a non-possible page
            if (page > pageAll) page = pageAll;
            if (!_memoryCache.TryGetValue($"User_{currentName}", out UserDataModel user)) // Caches the current user (admin)
            {
                user = await _context.GetUserByNameAsync(currentName);
                _memoryCache.Set($"User_{currentName}", user, TimeSpan.FromMinutes(3));
            }
            if (!_memoryCache.TryGetValue($"Page:{page}", out List<UserDataModel> users)) // Caches the page's users
            {
                users = await _context.Users.Skip((page - 1) * countUsersByPage)
                                            .Take(countUsersByPage)
                                            .ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                                                                     .SetSlidingExpiration(TimeSpan.FromMinutes(3));
                _memoryCache.Set($"Page:{page}", users, cacheEntryOptions);
            }
            bool firstPage = page == 1; // These 2 lines are for checking if NextPage or PreviousPage buttons should show
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
            if (userToDelete != default) // Checks if user's account is still present (in case he deleted it himself or something else unexpected happened)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
            _memoryCache.Remove("pageCount"); // Removes the user's count and therefore 
            return RedirectToAction("AdminMain", new { page });
        }
    }
}
