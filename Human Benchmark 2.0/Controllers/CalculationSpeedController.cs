using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Human_Benchmark_2._0.Controllers
{
    public class CalculationSpeedController : Controller
    {
        private readonly ILogger<CalculationSpeedController> _logger;
        private readonly ApplicationDbContext _context;
        public CalculationSpeedController(ILogger<CalculationSpeedController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Authorize]
        public IActionResult CalcSpeedMain()
        {
            try
            {
                return View();
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
        /// <param name="avgTime">Average time of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveCalc([FromBody] double avgTime)
        {
            try
            {
                UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
                userDataModel.AddCalcSpeedToArray(avgTime);
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
