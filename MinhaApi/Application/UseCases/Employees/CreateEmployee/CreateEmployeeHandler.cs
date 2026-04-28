
using MinhaApi.Application.DTOs;
using MinhaApi.Application.Interfaces;
using MinhaApi.Domain.employee.entitie;


namespace MinhaApi.Application.UseCases.Employees.CreateEmployee
{

    public class CreateEmployeeHandler
    {
        private readonly IEmployeeRepository _repo;
        private readonly IFileStorage _fileStorage;

        public CreateEmployeeHandler(
            IEmployeeRepository repo,
            IFileStorage fileStorage)
        {
            _repo = repo;
            _fileStorage = fileStorage;
        }

        public async Task<EmployeeResponse> Handle(CreateEmployeeCommand command)
        {
            if (await _repo.IsEmailAlreadyUse(command.email))
                throw new Exception("Email ja cadastrado no sistema");    

            string? filePath = null;

            if (command.photo != null)
            {
                filePath = await _fileStorage.SaveFileAsync(command.photo);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.password);

            var employee = new Employee(command.name, command.age, filePath, command.email, passwordHash);

            await _repo.Add(employee);

            return EmployeeResponse.ToResponse(employee);
        }
    }

}