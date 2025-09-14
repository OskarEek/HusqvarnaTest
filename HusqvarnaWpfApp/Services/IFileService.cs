
namespace HusqvarnaTest.Services
{
    public interface IFileService
    {
        public DateTime GetLastWriteTime(string filePath);

        public T? GetFileData<T>(string filePath);
    }
}
