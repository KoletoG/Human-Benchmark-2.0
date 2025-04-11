using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class ReverseNumbersController : Controller
    {
        private readonly ILogger<ReverseNumbersController> _logger;
        private readonly ApplicationDbContext _context;

        public ReverseNumbersController(ILogger<ReverseNumbersController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize]
        public IActionResult ReverseNumbersMain()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SaveNumbersScore([FromBody] int score)
        {
            UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
            userDataModel.AddReverseNumbersScoreToArray(score);
            _context.Update(userDataModel);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile", "Home") });
        }
    }
}
