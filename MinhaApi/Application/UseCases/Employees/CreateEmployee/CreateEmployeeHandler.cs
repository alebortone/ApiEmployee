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

        public async Task<Employee> Create(CreateEmployeeCommand command)
        {
            string? filePath = null;

            if (command.Photo != null)
            {
                filePath = await _fileStorage.SaveFileAsync(command.Photo);
            }

            var employee = new Employee(command.Name, command.Age, filePath);

            await _repo.Add(employee);

            return employee;
        }
    }

}