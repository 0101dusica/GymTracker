using GymTracker.Application.DTOs.Workout;
using GymTracker.Core.Entities;
using GymTracker.Infrastructure.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GymTracker.Application.Interfaces;

namespace GymTracker.API.Controllers
{
    [ApiController]
    [Route("api/workouts")]
    public class WorkoutSessionsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutSessionsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkoutSessionDto createDto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized();

            var workoutDto = await _workoutService.CreateAsync(userId, createDto);
            return CreatedAtAction(null, null, workoutDto);
            // ili možeš poslati samo Created bez location ako nema GetById
        }

        [Authorize]
        [HttpGet("monthly-summary")]
        public async Task<IActionResult> GetMonthlySummary([FromQuery] int month, [FromQuery] int year)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized();

            var referenceDate = new DateTime(year, month, 1);
            var summary = await _workoutService.GetMonthlySummaryAsync(userId, referenceDate);
            return Ok(summary);
        }
    }
}