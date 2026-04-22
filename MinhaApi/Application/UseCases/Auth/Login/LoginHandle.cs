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
        var employees = await _repository.GetAll();
        var employee = employees.FirstOrDefault(x =>
            x.Name.Equals(query.Name, StringComparison.OrdinalIgnoreCase) &&
            x.Age == query.Age);

        if (employee == null) return null;

  
        return _tokenService.GenerateToken(employee);
    }
}