using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class ReactionTimeController : Controller
    {
        private readonly ILogger<ReactionTimeController> _logger;
        private readonly ApplicationDbContext _context;

        public ReactionTimeController(ILogger<ReactionTimeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult ReactionTime()
        {
            return View("ReactionTime");
        }
        [Authorize]
        public async Task<IActionResult> ReactionTimeSave([FromBody] int time)
        {
            UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
            userDataModel.AddReactionTimeToArray(time);
            _context.Update(userDataModel);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile", "Home") });
        }
    }
}
