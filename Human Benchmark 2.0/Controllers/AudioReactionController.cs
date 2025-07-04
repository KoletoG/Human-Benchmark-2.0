﻿using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Interaces;
using Human_Benchmark_2._0.Models.DataModels;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Benchmark_2._0.Controllers
{
    public class AudioReactionController : Controller
    {
        private readonly ILogger<AudioReactionController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IIOService _ioService;
        private readonly IArrayAddService _arrayAddService;
        public AudioReactionController(ILogger<AudioReactionController> logger, ApplicationDbContext context, IIOService iOService, IArrayAddService arrayAddService)
        {
            _logger = logger;
            _context = context;
            _ioService = iOService;
            _arrayAddService = arrayAddService;
        }
        /// <summary>
        /// Loads the main page of the mini-game
        /// </summary>
        /// <returns>Main page of the mini-game</returns>
        [Route("AudioReactionGame")]
        public IActionResult AudioReactionMain()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(AudioReactionMain)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
        /// <summary>
        /// Saves AvgTime in database for the user
        /// </summary>
        /// <param name="avgTime">Average time of the game</param>
        /// <returns>Redirects to profile after completing</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAudioTime([FromBody] double avgTime)
        {
            try
            {
                var userDataModel = await _ioService.GetUserByNameAsync(this.User.Identity?.Name ?? "");
                _arrayAddService.AddTimeToArray(userDataModel.audioReactionAvgTimeArray,avgTime);
                _context.Attach(userDataModel);
                _context.Entry(userDataModel).Property(x => x.audioReactionAvgTimeArray).IsModified = true;
                await _context.SaveChangesAsync();
                return Json(new { redirectUrl = Url.Action("Profile", "Home") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {nameof(SaveAudioTime)}.");
                return View("ThrownException", new ThrownExceptionViewModel(ex, this.User.Identity?.Name ?? ""));
            }
        }
    }
}
