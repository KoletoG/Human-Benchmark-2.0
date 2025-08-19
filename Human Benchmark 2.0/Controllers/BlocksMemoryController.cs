using System.Security.Claims;
using System.Threading.Tasks;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Human_Benchmark_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class BlocksMemoryController : Controller
    {
        private readonly ILogger<BlocksMemoryController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        public BlocksMemoryController(ILogger<BlocksMemoryController> logger, ApplicationDbContext context, IIOService iOService)
        {
            _logger = logger;
            _context = context;
            _ioService = iOService;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Route("BlocksMemory")]
        public IActionResult BlocksMemoryMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(BlocksMemoryMain)}.");
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
        public async Task<IActionResult> SaveBlocksScore([FromBody] int score)
        {
            try
            {
                var user = await _ioService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                if(user is null)
                {
                    return Unauthorized();
                }
                ArrayAddService.AddScoreToArray(user.blocksScoreArray, score);
                _context.Attach(user);
                _context.Entry(user).Property(x => x.blocksScoreArray).IsModified = true;
                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(SaveBlocksScore)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
