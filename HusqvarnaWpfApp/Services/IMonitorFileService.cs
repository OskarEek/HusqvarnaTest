using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusqvarnaTest.Services
{
    public interface IMonitorFileService
    {
        public void CancelMonitoring();
        public void Dispose();

        event EventHandler? FileChanged;
    }
}
