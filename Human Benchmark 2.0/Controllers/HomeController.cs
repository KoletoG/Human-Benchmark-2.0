using System.Diagnostics;
using System.Threading.Tasks;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Human_Benchmark_2._0.Controllers
{ 
    // HUMAN BENCHMARK 2.0, Reaction time, memory numbers, memory words, calculation speed, pseudo IQ test,
  // reverse word, reverse number, blocks memory, coordination thoughtful test, reaction audio test, keyboard coordination test,
  // chess memory game, quiz millionaire, logical questions

    // Generate random name
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
        [Authorize]
        public IActionResult ReactionTime()
        {
            return View("ReactionTime");
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var currentUser = await _context.GetUserByNameAsync(this.User.Identity.Name);
            return View("Profile", currentUser);
        }
        [Authorize]
        public async Task<IActionResult> ReactionTimeSave([FromBody] int time)
        {
            UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
            userDataModel.AddReactionTimeToArray(time);
            _context.Update(userDataModel);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile","Home") });
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
