
using MinhaApi.Domain.employee.entitie;
using System.Reflection.Metadata;


namespace MinhaAPI.Aplication.Interfaces
{
    public interface ITokenService     
    {

        string GenerateToken(Employee employee);
    }
}
