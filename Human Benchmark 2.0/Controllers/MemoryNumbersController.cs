using Human_Benchmark_2._0.Data;
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
        public IActionResult MemoryNumbersSave()
        {
            return View("MemoryNumbersMain");
        }
    }
}
