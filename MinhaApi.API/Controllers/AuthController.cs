using Microsoft.AspNetCore.Mvc;
using MinhaAPI.Aplication.UseCases.Auth.Login;



namespace MinhaApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginHandler _authLogin;

        public AuthController(LoginHandler authLogin)
        {
            _authLogin = authLogin;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand request) 
        {
            
            var token = await _authLogin.Handle(request);

            if (token == null)
                return Unauthorized(new { message = "Nome ou Idade inválidos" });

            return Ok(token);
        }
    }
}
