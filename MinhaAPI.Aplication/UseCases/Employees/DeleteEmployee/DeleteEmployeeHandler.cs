using MinhaAPI.Aplication.Interfaces;

namespace MinhaAPI.Aplication.UseCases.Employees.DeleteEmployee
{
    public class DeleteEmployeeHandler
    {
        private readonly IEmployeeRepository _repo;

        public DeleteEmployeeHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteEmployeeCommand command)
        {
            await _repo.Delete(command.Id);
        }
    }
}
