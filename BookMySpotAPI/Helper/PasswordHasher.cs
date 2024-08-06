using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Identity;

namespace BookMySpotAPI.Helper
{
    public class PasswordHasher
    {
        private readonly IPasswordHasher<KorisnickiNalog> _passwordHasher;

        public PasswordHasher()
        {
            _passwordHasher = new PasswordHasher<KorisnickiNalog>();
        }

        public string HashPassword(KorisnickiNalog user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(KorisnickiNalog user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }
}
