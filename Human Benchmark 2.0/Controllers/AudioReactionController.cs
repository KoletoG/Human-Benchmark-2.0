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
        [Authorize]
        public IActionResult AudioReactionMain()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveAudioScore([FromBody] int score)
        {
            UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
            userDataModel.AddReverseWordsScoreToArray(score);
            _context.Update(userDataModel);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile", "Home") });
        }
    }
}
