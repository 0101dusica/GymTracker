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
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IWorkoutService _workoutService;

        public WorkoutSessionsController(AppDbContext db, IMapper mapper, IWorkoutService workoutService)
        {
            _db = db;
            _mapper = mapper;
            _workoutService = workoutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workouts = await _db.WorkoutSessions.ToListAsync();
            var workoutDtos = _mapper.Map<IEnumerable<WorkoutSessionDto>>(workouts);
            return Ok(workoutDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var workout = await _db.WorkoutSessions.FindAsync(id);
            if (workout == null) return NotFound();

            var workoutDto = _mapper.Map<WorkoutSessionDto>(workout);
            return Ok(workoutDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkoutSessionDto createDto)
        {
            var workout = _mapper.Map<WorkoutSession>(createDto);
            workout.Id = Guid.NewGuid();

            _db.WorkoutSessions.Add(workout);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = workout.Id }, createDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, WorkoutSessionDto updateDto)
        {
            var workout = await _db.WorkoutSessions.FindAsync(id);
            if (workout == null) return NotFound();

            _mapper.Map(updateDto, workout);

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var workout = await _db.WorkoutSessions.FindAsync(id);
            if (workout == null) return NotFound();

            _db.WorkoutSessions.Remove(workout);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpGet("monthly-summary")]
        public async Task<IActionResult> GetMonthlySummary([FromQuery] int month, [FromQuery] int year)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var referenceDate = new DateTime(year, month, 1);

            var summary = await _workoutService.GetMonthlySummaryAsync(userId, referenceDate);
            return Ok(summary);
        }

    }
}
