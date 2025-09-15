using System.IO;

namespace HusqvarnaTest.Services
{
    public class MonitorFileService : IMonitorFileService, IDisposable
    {
        private readonly string _monitoredFilePath;
        private readonly PeriodicTimer _periodicTimer;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private DateTime _lastFileWrite;

        public event EventHandler? FileChanged;

        public MonitorFileService(string filePath, TimeSpan? monitorInteval = null)
        {
            _monitoredFilePath = filePath;
            _cancellationTokenSource = new CancellationTokenSource();
            var inteval = monitorInteval ?? TimeSpan.FromSeconds(2);
            _periodicTimer = new PeriodicTimer(inteval);

            //TODO: if there was multiple places in the codebase that was going to use GetLastWriteTime or that I knew that I will have to do alot of other file-operations in other parts of the program in the future,
            //I would probobly have GetLastWriteTime as its own method in the FileService class instead to keep file operations centralized, but to keep it simpler in this specific program i decided not to.
            _lastFileWrite = File.GetLastWriteTimeUtc(_monitoredFilePath);
            _ = TimerLoop();
        }
        
        public void CancelMonitoring()
        {
            _cancellationTokenSource.Cancel();
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
            //TODO: Reading the file content every loop and comparing it to a old copy of it stored in memory would be the optimal way to check if the file data has changed or not.
            //I decided to not solve it this way since it becomes increasingly more expensive to both read the file-data every 2 seconds and to keep a old copy of it in memory all the time if the monitored file gets very big.
            //Although the File.GetLastWriteTimeUtc function comes with its own set of possible problems, (for example if the drive you're running the program on is formatted with FAT/FAT32 which has a date resolution
            //of 2 seconds for last modified time it can cause some issues, especially with the UnitTest) I still decided to solve it this way to avoid having to read the file-data every loop.
            //You could also possibly have solved this without having to read the file data in a better way by using FileSystemWatcher but unfortunately I did not have time to look into this.
            DateTime writeTime = File.GetLastWriteTimeUtc(_monitoredFilePath);
            return (writeTime != _lastFileWrite, writeTime);
        }

        private void UpdateLastFileWrite(DateTime writeTime)
        {
            _lastFileWrite = writeTime;
            FileChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _periodicTimer.Dispose();
            _cancellationTokenSource.Dispose();
        }
    }
}
