using System.IO;

namespace HusqvarnaTest.Services
{
    public class MonitorFileService : IMonitorFileService, IDisposable
    {
        private readonly string _monitoredFilePath;
        private readonly PeriodicTimer _periodicTimer;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private DateTime _lastFileWrite;

        public MonitorFileService(string filePath, TimeSpan? monitorInteval = null)
        {
            _monitoredFilePath = filePath;
            _cancellationTokenSource = new CancellationTokenSource();
            var inteval = monitorInteval ?? TimeSpan.FromSeconds(2);
            _periodicTimer = new PeriodicTimer(inteval);

            //TODO: if there was multiple places in the codebase that was going to use GetLastWriteTime or that I knew that I will have to do alot more file-operations,
            //I would probobly have GetLastWriteTime as its own method in the FileService class instead to keep file operations centralized, but to keep it simpler in this specific program i decided not to.
            _lastFileWrite = File.GetLastWriteTimeUtc(_monitoredFilePath);
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

        private (bool, DateTime) FileHasChanged()
        {
            DateTime writeTime = File.GetLastWriteTimeUtc(_monitoredFilePath);
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
