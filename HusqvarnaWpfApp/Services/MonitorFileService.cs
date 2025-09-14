using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusqvarnaTest.Services
{
    public class MonitorFileService : IMonitorFileService, IDisposable
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

        private async Task TimerLoop()
        {
            while (await _periodicTimer.WaitForNextTickAsync(_cancellationTokenSource.Token))
            {
                (bool fileHasChange, DateTime writeTime) = FileHasChanged();
                if (fileHasChange)
                {
                    UpdateLastFileWrite(writeTime);
                }
            }
        }

        internal (bool, DateTime) FileHasChanged()
        {
            DateTime writeTime = _fileService.GetLastWriteTime(_monitoredFilePath);
            return (writeTime != _lastFileWrite, writeTime);
        }

        private void UpdateLastFileWrite(DateTime writeTime)
        {
            _lastFileWrite = writeTime;
            FileChanged?.Invoke(this, EventArgs.Empty);
        }

        public void CancelMonitoring()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _periodicTimer.Dispose();
            _cancellationTokenSource.Dispose();
        }

        public event EventHandler? FileChanged;
    }
}
