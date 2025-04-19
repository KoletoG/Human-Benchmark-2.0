using System;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Methods;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
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
        /// <summary>
        /// Loads the main page of the mini-game                                                                                                                                                
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Authorize]
        public IActionResult MemoryWordsMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity.Name));
            }
        }
        /// <summary>
        /// Gets n random words from database
        /// </summary>
        /// <returns>string[] filled with n random words</returns>
        [HttpGet]
        public async Task<IActionResult> GetWords()
        {
            try
            {
                return Ok(await _context.GetRandomWordsAsync(150));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
        /// <summary>
        /// Saves Score in database for the user
        /// </summary>
        /// <param name="score">Score of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveWordsScore([FromBody] int score)
        {
            try
            {
                UserDataModel userDataModel = await _context.GetUserByNameAsync(this.User.Identity?.Name ?? "");
                userDataModel.memoryWordsScoreArray.AddValueToArray(score);
                _context.Update(userDataModel);
                _context.SaveChanges();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("ThrownException", new ThrownExceptionViewModel(ex,this.User.Identity?.Name ?? ""));
            }
        }
    }
}
