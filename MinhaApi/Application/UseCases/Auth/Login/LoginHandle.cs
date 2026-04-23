using MinhaApi.Application.Interfaces;
using MinhaApi.Infrastructure.Security;

namespace MinhaApi.Application.UseCases.Auth.Login;

public class LoginHandler
{
    private readonly IEmployeeRepository _repository;
    private readonly TokenService _tokenService;

    public LoginHandler(IEmployeeRepository repository, TokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    public async Task<object?> Handle(LoginCommand query)
    {
        var employee = await _repository.GetByEmail(query.Email);
       
        if (employee == null) return null;
        
        if (!BCrypt.Net.BCrypt.Verify(query.Password, employee.Password))
            return null;


        return _tokenService.GenerateToken(employee);
    }
}