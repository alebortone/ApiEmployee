using MinhaAPI.Aplication.DTOs;
using MinhaAPI.Aplication.Interfaces;
using MinhaAPI.Aplication.UseCases.Employees.GetEmployeeById;
using MinhaAPI.Aplication.UseCases.Employees.GetEmployees;
using MinhaAPI.Aplication.UseCases.Employees.GetPhotoEmployee;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhaAPI.Aplication.UseCases.Employees.GetPdfEmployee
{
    public class GetPdfEmployeeHandler
    {
        private readonly IEmployeeRepository _repoEmployee;
        private readonly IPdfService _pdfService;
        private readonly IFileStorage _fileStorage;

        public GetPdfEmployeeHandler(IEmployeeRepository repoEmployee, IPdfService pdfService, IFileStorage fileStorage )
        {
            _repoEmployee = repoEmployee;
            _pdfService = pdfService;
            _fileStorage = fileStorage;
        }

        public async Task<byte[]> Handler(GetPdfEmployeeQuery query)
        {
            var employee = await _repoEmployee.GetById(query.id);
            if (employee == null)
                return null;

            var photoEmployee = await _fileStorage.GetFileAsync(employee.Photo);
            return  _pdfService.GerarPdf(new EmployeePdfModel(employee.Name, employee.Age, photoEmployee));
        }

    }
}
