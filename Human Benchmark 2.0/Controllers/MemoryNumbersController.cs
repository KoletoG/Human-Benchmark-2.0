using System.Threading.Tasks;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class MemoryNumbersController : Controller
    {
        private readonly ILogger<MemoryNumbersController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        private readonly IArrayAddService _arrayAddService;
        public MemoryNumbersController(ILogger<MemoryNumbersController> logger, ApplicationDbContext context, IIOService iO, IArrayAddService arrayAddService)
        {
            _logger = logger;
            _context = context;
            _ioService = iO;
            _arrayAddService = arrayAddService;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Route("NumbersMemoryGame")]
        public IActionResult MemoryNumbers()
        {
            try
            {
                return View("MemoryNumbersMain");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(MemoryNumbers)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
        /// <summary>
        /// Saves Score in database for the user
        /// </summary>
        /// <param name="score">Score of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MemoryNumbersSave([FromBody] int score)
        {
            try
            {
                var user = await _ioService.GetUserByNameAsync(this.User.Identity.Name);
                _arrayAddService.AddValueToArray(user.memoryNumbersScoreArray, score);
                _context.Attach(user);
                _context.Entry(user).Property(x => x.memoryNumbersScoreArray).IsModified = true;
                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(MemoryNumbersSave)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
