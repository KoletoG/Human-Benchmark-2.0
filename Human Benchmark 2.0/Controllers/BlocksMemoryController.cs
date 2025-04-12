using Human_Benchmark_2._0.Data;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class BlocksMemoryController : Controller
    {
        private readonly ILogger<BlocksMemoryController> _logger;
        private readonly ApplicationDbContext _context;

        public BlocksMemoryController(ILogger<BlocksMemoryController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        // 7x7, let name += idBtn, **createElement("button")**, random elements 0-49 in array/list, timeout 5 sec, color = red, onclick()="failGame()", int clicked=0; 
        public IActionResult BlocksMemoryMain()
        {
            return View();
        }
    }
}
