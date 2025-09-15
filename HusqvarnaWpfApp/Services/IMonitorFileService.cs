
namespace HusqvarnaTest.Services
{
    public interface IMonitorFileService
    { 
        /// <summary>
        /// Occurs when the monitored file has been changed.
        /// </summary>
        event EventHandler? FileChanged;

        /// <summary>
        /// Stops the service from monitoring file.
        /// </summary>
        public void CancelMonitoring();

        public void Dispose();
    }
}
