using MinhaAPI.Aplication.Abstracoes.Filter;
using MinhaAPI.Aplication.DTOs;
using MinhaAPI.Aplication.Interfaces;
using System.Text.Json;

namespace MinhaAPI.Aplication.UseCases.Employees.GetEmployees
{
    public class GetEmployeesHandler
    {
        private readonly IEmployeeRepository _repoEmployee;

        public GetEmployeesHandler(IEmployeeRepository repo)
        {
            _repoEmployee = repo;
        }

        public async Task<PagedResponse<EmployeeResponse>> Handle(GetEmployeesQuery query)
        {
            List<FilterObject>? filters = null;

            if (!string.IsNullOrWhiteSpace(query.Filter))
            {
                filters = JsonSerializer.Deserialize<List<FilterObject>>(query.Filter);
            }


            var result = await _repoEmployee.GetPagined(
                query.Page,
                query.Limit,
                filters,
                query.OrderBy,
                query.OrderDirection
            );

            return new PagedResponse<EmployeeResponse>
            {
                Total = result.Total,
                Data = EmployeeResponse.FromList(result.Data)
            };
        }
    }
}
