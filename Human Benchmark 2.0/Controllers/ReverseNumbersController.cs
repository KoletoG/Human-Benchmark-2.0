using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class ReverseNumbersController : Controller
    {
        private readonly ILogger<ReverseNumbersController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        private readonly IArrayAddService _arrayAddService;

        public ReverseNumbersController(ILogger<ReverseNumbersController> logger, ApplicationDbContext context, IIOService iOService, IArrayAddService arrayAddService)
        {
            _logger = logger;
            _context = context;
            _ioService = iOService;
            _arrayAddService = arrayAddService;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Route("ReverseNumberGame")]
        public IActionResult ReverseNumbersMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(ReverseNumbersMain)}.");
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
        public async Task<IActionResult> SaveNumbersScore([FromBody] int score)
        {
            try
            {
                var userDataModel = await _ioService.GetUserByNameAsync(this.User.Identity?.Name ?? "");
                _arrayAddService.AddValueToArray(userDataModel.reverseNumbersScoreArray, score);
                _context.Attach(userDataModel);
                _context.Entry(userDataModel).Property(x => x.reverseNumbersScoreArray).IsModified = true;
                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(SaveNumbersScore)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
