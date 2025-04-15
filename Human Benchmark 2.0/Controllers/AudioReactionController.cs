using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class AudioReactionController : Controller
    {
        private readonly ILogger<AudioReactionController> _logger;
        private readonly ApplicationDbContext _context;

        public AudioReactionController(ILogger<AudioReactionController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Authorize]
        public IActionResult AudioReactionMain()
        {
            return View();
        }
        /// <summary>
        /// Saves AvgTime in database for the user
        /// </summary>
        /// <param name="avgTime">Average time of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveAudioTime([FromBody] int avgTime)
        {
            UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
            userDataModel.AddAudioReactionAvgToArray(avgTime);
            _context.Update(userDataModel);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile", "Home") });
        }
    }
}
