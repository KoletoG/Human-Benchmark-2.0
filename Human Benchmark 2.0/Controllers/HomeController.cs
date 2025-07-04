using System.Diagnostics;
using System.Threading.Tasks;
using Azure.Identity;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Human_Benchmark_2._0.Custom_Exceptions;
using Human_Benchmark_2._0.Interaces;

namespace Human_Benchmark_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,IIOService service)
        {
            _logger = logger;
            _context = context;
            _ioService = service;
        }
        /// <summary>
        /// Goes to the index page and fills database with words for the first time
        /// </summary>
        /// <returns>Index view and filled database</returns>
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(Index)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity.Name));
            }
        }
        /// <summary>
        /// Goes to the Profile page
        /// </summary>
        /// <returns>Profile view with UserDataModel Model of the current user</returns>
        [Authorize]
        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var currentUser = await _ioService.GetUserByNameAsync(this.User.Identity?.Name ?? throw new Exception("User not valid."));
                return View(new ProfileViewModel(currentUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(Profile)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex,this.User.Identity.Name));
            }
        }
        /// <summary>
        /// Goes to the Privacy page
        /// </summary>
        /// <returns>Privacy page view</returns>
        public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(Privacy)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity.Name));
            }
        }
        public IActionResult ThrownException()
        {
            return View(new ThrownExceptionViewModel(new Exception(), this.User.Identity.Name));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
