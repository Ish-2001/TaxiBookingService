using TaxiBookingService.Services.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace TaxiBookingService.Services.Service
{
    public class HashingService : IHashingService
    {
        public string Hashing(string password)
        {
            string reducedPassword = BC.HashPassword(password);
            return reducedPassword;
        }

        public bool VerifyHashing(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
