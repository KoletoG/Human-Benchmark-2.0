using Human_Benchmark_2._0.Data;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class ChessPuzzleController : Controller
    {

        private readonly ILogger<ChessPuzzleController> _logger;
        private readonly ApplicationDbContext _context;

        public ChessPuzzleController(ILogger<ChessPuzzleController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult ChessPuzzleMain()
        {
            return View();
        }
    }
}
