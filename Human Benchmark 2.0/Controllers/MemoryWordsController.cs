using System;
using System.Security.Claims;
using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Human_Benchmark_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class MemoryWordsController : Controller
    {
        private readonly ILogger<MemoryWordsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        public MemoryWordsController(ILogger<MemoryWordsController> logger, ApplicationDbContext context, IIOService iOService)
        {
            _logger = logger;
            _context = context;
            _ioService = iOService;
        }
        /// <summary>
        /// Loads the main page of the mini-game                                                                                                                                                
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Route("WordsMemoryGame")]
        [Authorize]
        public IActionResult MemoryWordsMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(MemoryWordsMain)}.");
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
                return Ok(await _ioService.GetRandomWordsAsync(150));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(GetWords)}.");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveWordsScore([FromBody] int score)
        {
            try
            {
                var user = await _ioService.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                if (user is null)
                {
                    return Unauthorized();
                }
                ArrayAddService.AddScoreToArray(user.memoryWordsScoreArray,score);
                _context.Attach(user);
                _context.Entry(user).Property(x => x.memoryWordsScoreArray).IsModified = true;
                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(SaveWordsScore)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex,this.User.Identity?.Name ?? ""));
            }
        }
    }
}
