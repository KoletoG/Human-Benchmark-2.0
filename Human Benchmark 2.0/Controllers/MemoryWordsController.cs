using System;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult MemoryWordsMain()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetWords()
        {
            return Ok(await _context.GetRandomWords(150));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveWordsScore([FromBody] int score)
        {
            UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
            userDataModel.AddScoreToWordsArray(score);
            _context.Update(userDataModel);
            _context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Profile", "Home") });
        }
    }
}
