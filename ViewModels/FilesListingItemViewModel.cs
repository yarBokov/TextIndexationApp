using IndexApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IndexApp.ViewModels
{
    public class FilesListingItemViewModel : ViewModelBase
    {
        public string FileName { get; }
        public ICommand DeleteCommand { get; }

        public FilesListingItemViewModel(FilesListingViewModel filesListingViewModel, string fileName)
        {
            FileName = fileName;
            DeleteCommand = new DeleteFileCommand(filesListingViewModel, FileName);
        }
    }
}
