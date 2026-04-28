

using MinhaAPI.Aplication.DTOs;

namespace MinhaAPI.Aplication.Interfaces
{
    public interface IPdfService
    {

        public byte[] GerarPdf(EmployeePdfModel employeeModel);
    }
}
