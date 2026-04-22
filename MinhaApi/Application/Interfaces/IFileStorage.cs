namespace MinhaApi.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<string> SaveFileAsync(IFormFile file);
        Task<byte[]> GetFileAsync(string path);
    }
}
