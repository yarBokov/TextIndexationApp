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
        private readonly string _fileToDelete;

        public DeleteFileCommand(FilesListingViewModel filesListingViewModel, string fileName) 
        { 
            _filesListingViewModel = filesListingViewModel;
            _fileToDelete = fileName;
        }

        public override void Execute(object? parameter)
        {
            _filesListingViewModel.RemoveItem(_fileToDelete);
        }
    }
}
