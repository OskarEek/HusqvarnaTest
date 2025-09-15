
namespace HusqvarnaTest.Services
{
    public interface IMonitorFileService
    {
        event EventHandler? FileChanged;
        public void CancelMonitoring();
        public void Dispose();
    }
}
