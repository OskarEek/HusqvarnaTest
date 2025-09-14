using System.IO;
using System.Text.Json;

namespace HusqvarnaTest.Services
{
    public class FileService : IFileService
    {

        public DateTime GetLastWriteTime(string filePath)
        {
            return File.GetLastWriteTime(filePath);
        }

        public T? GetFileData<T>(string filePath)
        {
            using (var r = new StreamReader(filePath))
            {
                try
                {
                    string json = r.ReadToEnd();
                    return JsonSerializer.Deserialize<T>(json);
                }
                catch (JsonException e)
                {
                    return default;
                }
            }
        }
    }
}
