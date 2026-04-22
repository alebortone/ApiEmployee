using Microsoft.IdentityModel.Tokens;
using MinhaApi.Domain.employee.entitie;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MinhaApi.Infrastructure.Security
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public object GenerateToken(Employee employee)
        {
            var secret = _configuration.GetSection("Settings").GetValue<string>("Secret");
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("employeeId", employee.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenHash = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenHash
            };
        }
    }
}
