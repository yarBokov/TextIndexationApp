using IndexApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.Commands
{
    public class DeleteFileCommand : CommandBase
    {
        private readonly FilesListingViewModel _filesListingViewModel;
        private readonly string _fileToDeleteNoPath;
        private readonly string _fileToDeleteFullPath;

        public DeleteFileCommand(FilesListingViewModel filesListingViewModel, string fileNameNoPath, string fileNameFullPath) 
        { 
            _filesListingViewModel = filesListingViewModel;
            _fileToDeleteNoPath = fileNameNoPath;
            _fileToDeleteFullPath = fileNameFullPath;
        }

        public override void Execute(object? parameter)
        {
            _filesListingViewModel.RemoveItem(_fileToDeleteNoPath, _fileToDeleteFullPath);
        }
    }
}
