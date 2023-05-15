using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using IndexApp.ViewModels.ItemViewModels;

namespace IndexApp.ViewModels
{
    public class FilesListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<FilesListingItemViewModel> _filesListingModels;
        public IEnumerable<FilesListingItemViewModel> Files => _filesListingModels;


        private FilesListingItemViewModel _item;
        public FilesListingItemViewModel Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public FilesListingViewModel()
        {
            _filesListingModels = new ObservableCollection<FilesListingItemViewModel>();
        }

        public void AddItem(string fileName) 
        {
            var fileNoPath = Path.GetFileName(fileName);
            foreach (var fileItemViewModel in _filesListingModels)
            {
                if (fileNoPath == fileItemViewModel.FileNameNoPath)
                    return;
            }
            Item = new FilesListingItemViewModel(this, fileNoPath, fileName);
            try
            {
                _filesListingModels.Add(Item);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Во время выполнения приложения возникло исключение: " + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void RemoveItem(string fileNameNoPath, string fileNameFullPath)
        {
            foreach (var fileItemViewModel in _filesListingModels)
            {
                if (fileNameNoPath == fileItemViewModel.FileNameNoPath)
                {
                    Item = fileItemViewModel;
                    _filesListingModels.Remove(Item);
                    return;
                }
            }
        }

        public List<string> getFullPathFileNames()
        {
            List<string> filesFullPaths = new List<string>();
            foreach(var fileItem in Files)
            {
                filesFullPaths.Add(fileItem.FileNameFullPath);
            }
            return filesFullPaths;
        }
    }
}
