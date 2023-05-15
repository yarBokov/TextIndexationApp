using IndexApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IndexApp.ViewModels.ItemViewModels
{
    public class FilesListingItemViewModel : ViewModelBase
    {
        public string FileNameNoPath { get; }
        public string FileNameFullPath { get; }
        public ICommand DeleteCommand { get; }

        public FilesListingItemViewModel(FilesListingViewModel filesListingViewModel, string fileNameNoPath, string fileNameFullPath)
        {
            FileNameNoPath = fileNameNoPath;
            FileNameFullPath = fileNameFullPath;
            DeleteCommand = new DeleteFileCommand(filesListingViewModel, FileNameNoPath, FileNameFullPath);
        }
    }
}
