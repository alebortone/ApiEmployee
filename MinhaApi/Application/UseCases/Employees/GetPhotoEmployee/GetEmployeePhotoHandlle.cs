using MinhaApi.Application.Interfaces;

namespace MinhaApi.Application.UseCases.Employees.GetPhotoEmployee
{
    public class GetEmployeePhotoHandler
    {
        private readonly IEmployeeRepository _repo;
        private readonly IFileStorage _fileStorage;

        public GetEmployeePhotoHandler(
            IEmployeeRepository repo,
            IFileStorage fileStorage)
        {
            _repo = repo;
            _fileStorage = fileStorage;
        }

        public async Task<byte[]?> Handle(GetEmployeePhotoQuery query)
        {
            var employee = await _repo.GetById(query.Id);

            if (employee == null || string.IsNullOrEmpty(employee.Photo))
                return null;

            return await _fileStorage.GetFileAsync(employee.Photo);
        }
    }
}
