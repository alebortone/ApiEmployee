using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinhaApi.Domain.employee.entitie;
using MinhaAPI.Aplication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MinhaApi.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Employee employee)
        {
          
            var handler = new JwtSecurityTokenHandler();

            var secretKey = _configuration["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JwtSettings:SecretKey não foi configurado.");
            var key = Encoding.ASCII.GetBytes(secretKey);

           
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
             
                new Claim(ClaimTypes.Name, employee.Name),
                new Claim("employeeId", employee.Id.ToString()),
                new Claim(ClaimTypes.Role, "Employee")
            }),
              
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}