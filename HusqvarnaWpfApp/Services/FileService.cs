using System.IO;
using System.Text.Json;

namespace HusqvarnaTest.Services
{
    public class FileService : IFileService
    {
        public T? GetJsonFileData<T>(string filePath)
        {
            using (var r = new StreamReader(filePath))
            {
                try
                {
                    string json = r.ReadToEnd();
                    return JsonSerializer.Deserialize<T>(json);
                }
                catch (JsonException)
                {
                    return default;
                }
            }
        }
    }
}
