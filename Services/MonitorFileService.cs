using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusqvarnaTest.Services
{
    class MonitorFileService : IMonitorFileService
    {
        private readonly string _monitoredFilePath;
        private readonly IFileService _fileService;

        private static readonly int _periodicTimerSeconds = 2;
        private readonly PeriodicTimer _periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(_periodicTimerSeconds));
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private DateTime _lastFileWrite;

        public MonitorFileService(string filePath, IFileService fileService)
        {
            _monitoredFilePath = filePath;
            _fileService = fileService;
            

            _lastFileWrite = _fileService.GetLastWriteTime(_monitoredFilePath);
            _ = TimerLoop();
        }

        public event EventHandler? FileChanged;

        private async Task TimerLoop()
        {
            try
            {
                while (await _periodicTimer.WaitForNextTickAsync(_cancellationTokenSource.Token))
                {

                    DateTime writeTime = _fileService.GetLastWriteTime(_monitoredFilePath);

                    if (writeTime != _lastFileWrite)
                    {
                        UpdateLastFileWrite(writeTime);
                    }
                }
            }
            catch
            {

            }
        }

        private void UpdateLastFileWrite(DateTime writeTime)
        {
            _lastFileWrite = writeTime;
            FileChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
