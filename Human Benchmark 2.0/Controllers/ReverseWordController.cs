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
        [Authorize]
        public IActionResult ReverseWordMain()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetWords()
        {
            return Ok(GlobalStaticMethods.GetRandomWords(350));
        }

        [Authorize]
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
