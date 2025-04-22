using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Human_Benchmark_2._0.Controllers
{
    public class ReverseWordController : Controller
    {
        private readonly ILogger<ReverseWordController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        private readonly IArrayAddService _arrayAddService;
        public ReverseWordController(ILogger<ReverseWordController> logger, ApplicationDbContext context, IIOService iOService, IArrayAddService arrayAddService)
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
        [Route("ReverseWordGame")]
        public IActionResult ReverseWordMain()
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
        /// Gets n random words from database
        /// </summary>
        /// <returns>string[] filled with n random words</returns>
        [HttpGet]
        public async Task<IActionResult> GetWords()
        {
            return Ok(await _ioService.GetRandomWordsAsync(350));
        }

        /// <summary>
        /// Saves Score in database for the user
        /// </summary>
        /// <param name="score">Score of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveWordsScore([FromBody] int score)
        {
            try
            {
                var userDataModel = await _ioService.GetUserByNameAsync(this.User.Identity?.Name ?? "");
                _arrayAddService.AddValueToArray(userDataModel.reverseWordsScoreArray, score);
                _context.Attach(userDataModel);
                _context.Entry(userDataModel).Property(x => x.reverseWordsScoreArray).IsModified = true;
                await _context.SaveChangesAsync();
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
