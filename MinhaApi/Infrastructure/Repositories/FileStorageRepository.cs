using MinhaApi.Application.Interfaces;

namespace MinhaApi.Infrastructure.Repositories
{
    public class FileStorageRepository : IFileStorage
    {
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var path = Path.Combine("Storage", file.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return path;
        }

        public async Task<byte[]> GetFileAsync(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }
    }
}
