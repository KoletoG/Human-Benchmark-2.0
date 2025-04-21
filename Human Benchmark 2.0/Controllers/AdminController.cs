using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
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
        private readonly ICacheService _cacheService;
        private readonly IIOService _ioService;
        public AdminController(ILogger<AdminController> logger,IIOService iOService, ApplicationDbContext context, IMemoryCache memoryCache, ICacheService cacheService)
        {
            _logger = logger;
            _context = context;
            _memoryCache = memoryCache;
            _cacheService = cacheService;
            _ioService = iOService;
        }
        
        // Add searching by name
        // Add comments
        [Authorize]
        public async Task<IActionResult> AdminMain(int page = 1)
        {
            string currentName = this.User.Identity?.Name ?? "";
            if (currentName != Constants.Constants.adminName)
            {
                return RedirectToAction("Index", "Home");
            }
            int countUsersByPage = 2; // Sets how many users should be shown per page
            int countUsers = await _context.Users.CountAsync();
            _cacheService.HandleCacheAdmin(countUsers, countUsersByPage);
            int pageAll = (int)Math.Ceiling((double)countUsers / countUsersByPage);
            _memoryCache.Set("pageCount", pageAll, TimeSpan.FromMinutes(5)); // Saves pageCount and count in cache only for 5 minutes in case a user has deleted their account at the same time somebody has created one so the algorithm cannot catch the difference
            _memoryCache.Set("count", countUsers, TimeSpan.FromMinutes(5));
            if (page < 1) page = 1; // These 2 lines handle exceptions if user has manually entered a non-possible page
            if (page > pageAll) page = pageAll;
            if (!_memoryCache.TryGetValue($"User_{currentName}", out UserDataModel user)) // Caches the current user (admin)
            {
                user = await _ioService.GetUserByNameAsync(currentName);
                _memoryCache.Set($"User_{currentName}", user, TimeSpan.FromMinutes(3));
            }
            if (!_memoryCache.TryGetValue($"Page:{page}", out List<UserDataModel> users)) // Caches the page's users
            {
                users = await _context.Users.AsNoTracking().Skip((page - 1) * countUsersByPage)
                                            .Take(countUsersByPage)
                                            .ToListAsync(); // Loads users per page
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
            return RedirectToAction(nameof(AdminMain), new { page });
        }
    }
}
