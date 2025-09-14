
namespace HusqvarnaTest.Services
{
    public interface IMonitorFileService
    {
        public void CancelMonitoring();
        public void Dispose();

        event EventHandler? FileChanged;
    }
}
