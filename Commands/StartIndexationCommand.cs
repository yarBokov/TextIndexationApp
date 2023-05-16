using IndexApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.Commands
{
    public class StartIndexationCommand : CommandBase
    {
        private readonly FilesListingViewModel _filesListing;
        private readonly IndexationResultViewModel _result;

        public StartIndexationCommand(FilesListingViewModel filesListingViewModel, IndexationResultViewModel indexationResultViewModel)
        {
            _filesListing=filesListingViewModel;
            _result = indexationResultViewModel;
        }

        public override void Execute(object? parameter)
        {
            _result.Clear();
            var indexator = new IndexatorUtility(_filesListing);
            _result.InsertResults(indexator.getResults());
        }
    }
}
