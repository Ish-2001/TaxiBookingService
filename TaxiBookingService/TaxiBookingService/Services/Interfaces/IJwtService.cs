namespace TaxiBookingService.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(string userName, string password);
    }
}
