using Microsoft.AspNetCore.Http;
using MinhaApi.Domain.employee.entitie;

namespace MinhaAPI.Aplication.UseCases.Employees.UpdateEmployee
{
    public record UpdateEmployeeCommand(string name, int age, string email, IFormFile? photo)
    {
    }
}
