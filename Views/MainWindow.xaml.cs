using HusqvarnaTest.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

    private void QuitButton(object sender, RoutedEventArgs e)
    {
        (DataContext as MainViewModel)?.QuitButton();
    }
}