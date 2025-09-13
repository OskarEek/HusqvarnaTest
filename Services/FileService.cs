using HusqvarnaTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HusqvarnaTest.Services
{
    class FileService : IFileService
    {

        public DateTime GetLastWriteTime(string filePath)
        {
            return File.GetLastWriteTime(filePath);
        }

        public T? GetFileData<T>(string filePath)
        {
            using (var r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
