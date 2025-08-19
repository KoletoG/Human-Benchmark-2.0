using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Human_Benchmark_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class ReactionTimeController : Controller
    {
        private readonly ILogger<ReactionTimeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;

        public ReactionTimeController(ILogger<ReactionTimeController> logger, ApplicationDbContext context, IIOService iOService)
        {
            _logger = logger;
            _context = context;
            _ioService = iOService;
        }

        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Route("ReactionTimeGame")]
        public IActionResult ReactionTime()
        {
            try
            {
                return View("ReactionTime");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(ReactionTime)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
        /// <summary>
        /// Saves AvgTime in database for the user
        /// </summary>
        /// <param name="time">Average time of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReactionTimeSave([FromBody] double time)
        {
            try
            {
                var userDataModel = await _ioService.GetUserByNameAsync(this.User.Identity?.Name ?? "");
                ArrayAddService.AddTimeToArray(userDataModel.reactionTimesArray, time);
                _context.Attach(userDataModel);
                _context.Entry(userDataModel).Property(x => x.reactionTimesArray).IsModified = true;
                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(ReactionTimeSave)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
