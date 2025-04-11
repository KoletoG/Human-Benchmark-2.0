using Human_Benchmark_2._0.Data;
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
        public IActionResult ReverseWordMain()
        {
            return View();
        }
    }
}
