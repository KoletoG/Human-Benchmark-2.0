using Human_Benchmark_2._0.Data;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class MemoryWordsController : Controller
    {
        private readonly ILogger<MemoryWordsController> _logger;
        private readonly ApplicationDbContext _context;

        public MemoryWordsController(ILogger<MemoryWordsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult MemoryWordsMain()
        {
            return View();
        }
    }
}
