using GymTracker.Application.DTOs.User;
using GymTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userDto = await _userService.GetCurrentUserAsync(User);
            return userDto == null ? Unauthorized() : Ok(userDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto dto)
        {
            var result = await _userService.UpdateAsync(dto, User);
            return result ? NoContent() : Unauthorized();
        }
    }

}
