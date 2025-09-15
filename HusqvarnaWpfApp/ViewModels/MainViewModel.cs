using HusqvarnaTest.Models;
using HusqvarnaTest.Services;
using System.Collections.ObjectModel;

namespace HusqvarnaTest.ViewModels
{
    class MainViewModel
    {
        private readonly string _filePath;
        private readonly IMonitorFileService _monitorFileService;
        private readonly IFileService _fileService;
        public ObservableCollection<CompanyModel> Companies { get; }

        public MainViewModel(string filePath, IMonitorFileService monitorFileService, IFileService fileService)
        {
            _filePath = filePath;
            _monitorFileService = monitorFileService;
            _fileService = fileService;
            Companies = new();

            RefreshFileData();
            _monitorFileService.FileChanged += (s, e) =>
            {
                RefreshFileData();
            };
        }

        //TODO: There are better ways to handle button clicks (for example using ICommand) but becuase of some knowledge gaps when it comes to WPF and not enough time to research it, I decided solved this in a suboptimal way
        public void ForceRefreshDataButton()
        {
            RefreshFileData();
        }

        public void CancelMonitoringButton()
        {
            _monitorFileService.CancelMonitoring();
        }

        private void RefreshFileData()
        {
            Companies.Clear();
            var result = _fileService.GetFileData<List<CompanyModel>>(_filePath) ?? new();
            foreach (var company in result)
            {
                Companies.Add(company);
            }
        }
    }
}
