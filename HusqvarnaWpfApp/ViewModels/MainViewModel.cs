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
        private readonly IMonitorFileService _monitorFileService;
        private readonly IFileService _fileService;
        public ObservableCollection<CompanyModel> Companies { get; } = new();

        public MainViewModel()
        {
            _fileService = new FileService();
            _monitorFileService = new MonitorFileService(_filePath);
            RefreshFileData();
            _monitorFileService.FileChanged += (s, e) =>
            {
                RefreshFileData();
            };
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
        public void ForceRefreshDataButton()
        {
            RefreshFileData();
        }

        public void CancelMonitoringButton()
        {
            _monitorFileService.CancelMonitoring();
        }
        public void QuitButton()
        {
            throw new NotImplementedException();
        }

    }
}
