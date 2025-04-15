using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class ReverseWordController : Controller
    {
        private readonly ILogger<ReverseWordController> _logger;
        private readonly ApplicationDbContext _context;

        public ReverseWordController(ILogger<ReverseWordController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Authorize]
        public IActionResult ReverseWordMain()
        {
            return View();
        }
        /// <summary>
        /// Gets n random words from database
        /// </summary>
        /// <returns>string[] filled with n random words</returns>
        [HttpGet]
        public async Task<IActionResult> GetWords()
        {
            return Ok(await _context.GetRandomWords(350));
        }

        /// <summary>
        /// Saves Score in database for the user
        /// </summary>
        /// <param name="score">Score of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveWordsScore([FromBody] int score)
        {
            UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
            userDataModel.AddReverseWordsScoreToArray(score);
            _context.Update(userDataModel);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile", "Home") });
        }
    }
}
