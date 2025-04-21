using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class ReactionTimeController : Controller
    {
        private readonly ILogger<ReactionTimeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        private readonly IArrayAddService _arrayAddService;

        public ReactionTimeController(ILogger<ReactionTimeController> logger, ApplicationDbContext context, IIOService iOService, IArrayAddService arrayAddService)
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
        [Authorize]
        public IActionResult ReactionTime()
        {
            try
            {
                return View("ReactionTime");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
        /// <summary>
        /// Saves AvgTime in database for the user
        /// </summary>
        /// <param name="time">Average time of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        public async Task<IActionResult> ReactionTimeSave([FromBody] int time)
        {
            try
            {
                UserDataModel userDataModel = await _ioService.GetUserByNameAsync(this.User.Identity?.Name ?? "");
                _arrayAddService.AddValueToArray(userDataModel.reactionTimesArray, time);
                _context.Update(userDataModel);
                _context.SaveChanges();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
