using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Human_Benchmark_2._0.Controllers
{
    public class CalculationSpeedController : Controller
    {
        private readonly ILogger<CalculationSpeedController> _logger;
        private readonly ApplicationDbContext _context;
        public CalculationSpeedController(ILogger<CalculationSpeedController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult CalcSpeedMain()
        {
            return View();
        }
      
    }
}
