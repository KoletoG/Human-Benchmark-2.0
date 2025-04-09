using System.Threading.Tasks;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class MemoryNumbersController : Controller
    {
        private readonly ILogger<MemoryNumbersController> _logger;
        private readonly ApplicationDbContext _context;

        public MemoryNumbersController(ILogger<MemoryNumbersController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize]
        public IActionResult MemoryNumbers()
        {
            return View("MemoryNumbersMain");
        }
        [HttpPost]
        public async Task<IActionResult> MemoryNumbersSave([FromBody] int score)
        {
            var user = await _context.GetUserByNameAsync(this.User.Identity.Name);
            user.AddScoreToMemoryNumbersArray(score);
            _context.Update(user);
            _context.SaveChanges();
            return Json(new {redirectUrl= Url.Action("Profile","Home")});
        }
    }
}
