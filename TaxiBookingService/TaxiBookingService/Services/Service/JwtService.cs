using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkUser _unitOfWork;

        public JwtService(IConfiguration configuration, IUnitOfWorkUser unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public string GenerateToken( string userName, string password)
        {
            User user = _unitOfWork.Users.GetAll().Where(item => item.UserName == userName).FirstOrDefault();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string claim;

            if (user.UserRole.Role == Role.User.ToString())
            {
                claim = Role.User.ToString();
            }
            else if(user.UserRole.Role == Role.Admin.ToString())
            {
                claim = Role.Admin.ToString();
            }
            else
            {
                claim = Role.Driver.ToString();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                 new Claim(ClaimTypes.Role,claim)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"]
            , _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(12), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
