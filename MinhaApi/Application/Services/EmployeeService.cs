using MinhaApi.Application.ViewModel;
using MinhaApi.Domain.employee.entitie;
using MinhaApi.Domain.employee.Interface;

namespace MinhaApi.Application.Services
{
    public class EmployeeService : ServiceGeneric<Employee, EmployeeViewModel>
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeService(IEmployeeRepository repository): base(repository)
        {
            _employeeRepo = repository;
        }

        public async Task<Employee> Create(EmployeeViewModel employeeViewModel)
        {
            string? filePath = null;

            if (employeeViewModel.Photo != null)
            {
                filePath = Path.Combine("Storage", employeeViewModel.Photo.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                employeeViewModel.Photo.CopyTo(fileStream);
            }
           
            var employee = new Employee(employeeViewModel.Name, employeeViewModel.Age, filePath);
            await _employeeRepo.Add(employee);
            return employee;
        }
         
        public async Task<byte[]> GetPhotoBytes(int id)
        {
            return await _employeeRepo.GetPhotoBytesAsync(id);
        }
    }
}
