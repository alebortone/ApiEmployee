using MinhaApi.Application.Interfaces;

namespace MinhaApi.Application.UseCases.Employees.DeleteEmployee
{
    public class DeleteEmployeeHandler
    {
        private readonly IEmployeeRepository _repo;

        public DeleteEmployeeHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task Delete(DeleteEmployeeCommand command)
        {
            await _repo.Delete(command.Id);
        }
    }
}
