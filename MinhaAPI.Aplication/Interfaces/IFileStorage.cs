using Microsoft.AspNetCore.Http;

namespace MinhaAPI.Aplication.Interfaces
{
    public interface IFileStorage
    {
        Task<string> SaveFileAsync(IFormFile file);
        Task<byte[]> GetFileAsync(string path);
    }
}
