using Human_Benchmark_2._0.Data;
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
        public IActionResult MemoryNumbersSave([FromBody] int score)
        {
            return Json(new {redirectUrl= Url.Action("Profile","Home")});
        }
    }
}
