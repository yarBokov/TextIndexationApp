using IndexApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IndexApp.ViewModels
{
    public class IndexationStartViewModel : ViewModelBase
    {
        public FilesListingViewModel FilesListingViewModel { get; }
        public UniqueWordsViewModel UniqueWordsViewModel { get; }
        public FileWordPosListingViewModel FileWordPosListingViewModel { get; }
        public WordFilesListingViewModel WordFilesListingViewModel { get; }

        public ICommand AddFilesCommand { get; }
        public ICommand StartIndexationCommand { get; }

        public IndexationStartViewModel()
        {
            FilesListingViewModel = new FilesListingViewModel();
            UniqueWordsViewModel = new UniqueWordsViewModel();
            FileWordPosListingViewModel= new FileWordPosListingViewModel();
            WordFilesListingViewModel= new WordFilesListingViewModel();

            AddFilesCommand = new AddFilesCommand(FilesListingViewModel);
            StartIndexationCommand = new StartIndexationCommand(FilesListingViewModel, UniqueWordsViewModel,FileWordPosListingViewModel, WordFilesListingViewModel);
        }
    }
}
