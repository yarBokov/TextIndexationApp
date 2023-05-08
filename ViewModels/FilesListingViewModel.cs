using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.ViewModels
{
    public class FilesListingViewModel : ViewModelBase
    {
        private FilesListingItemViewModel _item;
        private readonly ObservableCollection<FilesListingItemViewModel> _filesListingModels;
        public IEnumerable<FilesListingItemViewModel> Files => _filesListingModels;

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
            foreach (var fileItemViewModel in _filesListingModels)
            {
                if (fileName == fileItemViewModel.FileName)
                    return;
            }
            Item = new FilesListingItemViewModel(this, fileName);
            _filesListingModels.Add(Item);
        }

        public void RemoveItem(string fileName)
        {
            foreach (var fileItemViewModel in _filesListingModels)
            {
                if (fileName == fileItemViewModel.FileName)
                {
                    Item = fileItemViewModel;
                    _filesListingModels.Remove(Item);
                    return;
                }
            }
        }
    }
}
