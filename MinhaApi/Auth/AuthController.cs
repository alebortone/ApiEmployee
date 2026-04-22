using Microsoft.AspNetCore.Mvc;
using MinhaApi.Application.Services;

namespace MinhaApi.Auth
{

    public class LoginRequest
    {
        public  string Name { get; set; }
        public  int Age { get; set; }
    }


    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) // Exemplo simples
        {
            
            var token = await _authService.Authenticate(request.Name, request.Age);

            if (token == null)
                return Unauthorized(new { message = "Nome ou Idade inválidos" });

            return Ok(token);
        }
    }
}
