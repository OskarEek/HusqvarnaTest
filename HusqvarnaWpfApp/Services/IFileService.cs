using HusqvarnaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusqvarnaTest.Services
{
    public interface IFileService
    {
        public DateTime GetLastWriteTime(string filePath);

        public T? GetFileData<T>(string filePath);
    }
}
