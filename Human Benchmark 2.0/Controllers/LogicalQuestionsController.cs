using Human_Benchmark_2._0.Data;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class LogicalQuestionsController : Controller
    {
        private readonly ILogger<LogicalQuestionsController> _logger;
        private readonly ApplicationDbContext _context;

        public LogicalQuestionsController(ILogger<LogicalQuestionsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult LogicalQuestionsMain()
        {
            return View();
        }
    }
}
