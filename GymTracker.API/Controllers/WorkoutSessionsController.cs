using GymTracker.Application.DTOs.Workout;
using GymTracker.Core.Entities;
using GymTracker.Infrastructure.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.API.Controllers
{
    [ApiController]
    [Route("api/workouts")]
    public class WorkoutSessionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public WorkoutSessionsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
    }
}
