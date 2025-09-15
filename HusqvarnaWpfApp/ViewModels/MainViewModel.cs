using System.Collections.ObjectModel;
using HusqvarnaTest.Models;
using HusqvarnaTest.Services;

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
            Companies = new ObservableCollection<CompanyModel>();

            RefreshFileData();
            _monitorFileService.FileChanged += (s, e) =>
            {
                RefreshFileData();
            };
        }

        //TODO: There are better ways to handle button clicks (for example using ICommand) but becuase of some knowledge gaps when it comes to WPF and not enough time to research it, I decided solved this in a suboptimal way
        /// <summary>
        /// Handles button click from ForceRefreshDataButton
        /// </summary>
        public void ForceRefreshDataButton()
        {
            RefreshFileData();
        }

        /// <summary>
        /// Handles button click from CancelMonitoring button
        /// </summary>
        public void CancelMonitoringButton()
        {
            _monitorFileService.CancelMonitoring();
        }
        
        //Refreshes UI with data from file with path _filePath
        private void RefreshFileData()
        {
            Companies.Clear();
            List<CompanyModel>? result = _fileService.GetJsonFileData<List<CompanyModel>>(_filePath) ?? new();
            foreach (var company in result)
            {
                Companies.Add(company);
            }
        }
    }
}
