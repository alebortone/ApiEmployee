using Microsoft.AspNetCore.Http;

namespace MinhaAPI.Aplication.UseCases.Employees.CreateEmployee
{
    public record CreateEmployeeCommand(
     string name,
     int age,
     IFormFile? photo,
     string email,
     string password

 );
}
