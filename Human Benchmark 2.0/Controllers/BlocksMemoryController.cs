using System.Threading.Tasks;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class BlocksMemoryController : Controller
    {
        private readonly ILogger<BlocksMemoryController> _logger;
        private readonly ApplicationDbContext _context;

        public BlocksMemoryController(ILogger<BlocksMemoryController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult BlocksMemoryMain()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveBlocksScore([FromBody] int score)
        {
            var user = await _context.GetUserByNameAsync(this.User.Identity.Name);
            user.AddScoreBlocksToArray(score);
            _context.Update(user);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile", "Home") });
        }
    }
}
