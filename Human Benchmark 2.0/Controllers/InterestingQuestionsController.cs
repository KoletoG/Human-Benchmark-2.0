using Human_Benchmark_2._0.Custom_Exceptions;
using Human_Benchmark_2._0.Data;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class InterestingQuestionsController : Controller
    {
        private readonly ILogger<InterestingQuestionsController> _logger;
        private readonly ApplicationDbContext _context;

        public InterestingQuestionsController(ILogger<InterestingQuestionsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult InterestingQuestionsMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error","Home");
            }
        }
    }
}
