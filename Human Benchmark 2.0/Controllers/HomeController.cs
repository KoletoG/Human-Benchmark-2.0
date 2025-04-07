using System.Diagnostics;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{ 
    // HUMAN BENCHMARK 2.0, Reaction time, memory numbers, memory words, calculation speed, pseudo IQ test,
  // reverse word, reverse number, blocks memory, coordination thoughtful test, reaction audio test, keyboard coordination test,
  // chess memory game, quiz millionaire, logical questions
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ReactionTime()
        {
            return View("ReactionTime");
        }
        public IActionResult ReactionTimeSave([FromBody] int time)
        {
            
            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
