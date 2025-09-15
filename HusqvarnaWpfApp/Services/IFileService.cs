
namespace HusqvarnaTest.Services
{
    public interface IFileService
    {
        public T? GetFileData<T>(string filePath);
    }
}
