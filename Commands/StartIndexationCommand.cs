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
        private readonly UniqueWordsViewModel _uniqueWordsViewModel;
        private readonly FileWordPosListingViewModel _fileWordPosListingViewModel;
        private readonly WordFilesListingViewModel _wordFilesListingViewModel;

        public StartIndexationCommand(FilesListingViewModel filesListingViewModel, UniqueWordsViewModel uniqueWordsViewModel, 
            FileWordPosListingViewModel fileWordPosListingViewModel, 
            WordFilesListingViewModel wordFilesListingViewModel)
        {
            _filesListing=filesListingViewModel;
            _uniqueWordsViewModel = uniqueWordsViewModel;
            _fileWordPosListingViewModel = fileWordPosListingViewModel;
            _wordFilesListingViewModel = wordFilesListingViewModel;
        }

        public override void Execute(object? parameter)
        {
            clearLists();
            var indexator = new IndexatorUtility(_filesListing);
            _uniqueWordsViewModel.InsertWords(indexator.UniqueWords.ToList());
        }

        private void clearLists()
        {
            _uniqueWordsViewModel.Clear();
        }
    }
}
