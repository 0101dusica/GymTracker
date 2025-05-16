using AutoMapper;
using GymTracker.Application.DTOs.User;
using GymTracker.Core.Entities;
using GymTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UsersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _db.Users.ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UserResponseDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            var userDto = _mapper.Map<UserResponseDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.Id = Guid.NewGuid();
            user.IsActive = false;

            // password need to be hashed before saving
            // user.PasswordHash = _passwordHasher.HashPassword(registerDto.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var userDto = _mapper.Map<UserResponseDto>(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserRegisterDto updateDto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            _mapper.Map(updateDto, user);
            // if password changed - hashed before saving

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
