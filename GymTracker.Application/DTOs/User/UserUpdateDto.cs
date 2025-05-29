namespace GymTracker.Application.DTOs.User
{
    public class UserUpdateDto
    {         
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Gender { get; set; }
    }
}
