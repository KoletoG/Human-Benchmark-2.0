using System.Security.Claims;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Human_Benchmark_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Human_Benchmark_2._0.Controllers
{
    public class CalculationSpeedController : Controller
    {
        private readonly ILogger<CalculationSpeedController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        public CalculationSpeedController(ILogger<CalculationSpeedController> logger, ApplicationDbContext context, IIOService iO)
        {
            _logger = logger;
            _context = context;
            _ioService = iO;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Route("CalculationSpeedGame")]
        public IActionResult CalcSpeedMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(CalcSpeedMain)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
        /// <summary>
        /// Saves AvgTime in database for the user
        /// </summary>
        /// <param name="avgTime">Average time of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCalc([FromBody] double avgTime)
        {
            try
            {
                var user = await _ioService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                if (user is null)
                {
                    return Unauthorized();
                }
                ArrayAddService.AddTimeToArray(user.avgTimeScoreArray, avgTime);
                _context.Attach(user);
                _context.Entry(user).Property(x => x.avgTimeScoreArray).IsModified = true;
                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(SaveCalc)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
