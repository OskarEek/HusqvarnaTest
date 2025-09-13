using HusqvarnaTest.Models;
using HusqvarnaTest.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HusqvarnaTest.ViewModels
{
    class MainViewModel
    {
        private static readonly string _filePath = "data.json";
        private readonly IMonitorFileService _fileMonitorService;
        private readonly IFileService _fileService;
        public ObservableCollection<CompanyModel> Companies { get; } = new();

        public MainViewModel()
        {
            _fileService = new FileService();
            _fileMonitorService = new MonitorFileService(_filePath, _fileService);
            UpdateFileData();
            _fileMonitorService.FileChanged += (s, e) =>
            {
                UpdateFileData();
            };
        }


        private void UpdateFileData()
        {
            Companies.Clear();
            var result = _fileService.GetFileData<List<CompanyModel>>(_filePath) ?? new();
            foreach (var company in result)
            {
                Companies.Add(company);
            }
        }
        public void ForceRefreshDataButton()
        {
            UpdateFileData();
        }

        public void CancelMonitoringButton()
        {
            _fileMonitorService.CancelMonitoring();
        }
        public void QuitButton()
        {
            throw new NotImplementedException();
        }

    }
}
