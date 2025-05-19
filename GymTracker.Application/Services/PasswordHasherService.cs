using GymTracker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GymTracker.Application.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<object> _passwordHasher = new();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
