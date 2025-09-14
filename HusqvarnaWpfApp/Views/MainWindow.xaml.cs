using HusqvarnaTest.ViewModels;
using System.Windows;

namespace HusqvarnaTest.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ForceRefreshDataButton(object sender, RoutedEventArgs e)
    {
        // Trigger ViewModel method
        (DataContext as MainViewModel)?.ForceRefreshDataButton();
    }

    private void CancelMonitoringButton(object sender, RoutedEventArgs e)
    {
        (DataContext as MainViewModel)?.CancelMonitoringButton();
    }
}