using HusqvarnaTest.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HusqvarnaTest;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static string _filePath = "data.json";
    private IMonitorFileService? _monitorFileService;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        //TODO: I would rather solve this using dependency injection but unfortunately I did not have time try that this time
        _monitorFileService = new MonitorFileService(_filePath);
        var fileService = new FileService();

        var window = new Views.MainWindow()
        {
            DataContext = new ViewModels.MainViewModel(_filePath, _monitorFileService, fileService)
        };
        window.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _monitorFileService?.Dispose();
        base.OnExit(e);
    }
}

