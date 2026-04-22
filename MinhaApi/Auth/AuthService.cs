using MinhaApi.Domain.employee.Interface;

namespace MinhaApi.Auth
{
    public class AuthService
    {
        private readonly IEmployeeRepository _repository;
        private readonly TokenService _tokenService;

        public AuthService(IEmployeeRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<object?> Authenticate(string name, int age)
        {
            
            var employees = await _repository.GetAll();
            var employee = employees.FirstOrDefault(x =>
                x.name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                x.age == age); 

            if (employee == null)
                return null;

            var token = _tokenService.GenerateToken(employee);

            return token;
        }
    }
}
