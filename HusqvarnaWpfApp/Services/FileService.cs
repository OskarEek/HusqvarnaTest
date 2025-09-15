using System.IO;
using System.Text.Json;

namespace HusqvarnaTest.Services
{
    public class FileService : IFileService
    {
        public T? GetJsonFileData<T>(string filePath)
        {
            try
            {
                using (var r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    return JsonSerializer.Deserialize<T>(json);
                }
            }
            catch (JsonException)
            {
                return default;
            }
            catch (FileNotFoundException)
            {
                return default;
            }
        }
    }
}
