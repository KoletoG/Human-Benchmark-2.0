using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Models.ViewModels;
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
        [Route("LogicalQuestionsGame")]
        public IActionResult LogicalQuestionsMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
