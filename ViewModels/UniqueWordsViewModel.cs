using IndexApp.ViewModels.ItemViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.ViewModels
{
    public class UniqueWordsViewModel : ViewModelBase
    {
        private ObservableCollection<UniqueWordsItemViewModel> _uniqueWordsModels;
        public IEnumerable<UniqueWordsItemViewModel> Words => _uniqueWordsModels;

        private UniqueWordsItemViewModel _item;
        public UniqueWordsItemViewModel Item
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

        public UniqueWordsViewModel()
        {
            _uniqueWordsModels= new ObservableCollection<UniqueWordsItemViewModel>();
        }

        public void InsertWords(List<string> wordsSet)
        {
            foreach(var word in wordsSet)
            {
                Item = new UniqueWordsItemViewModel(word);
                _uniqueWordsModels.Add(Item);
            }
        }

        public void Clear()
        {
            _uniqueWordsModels.Clear();
        }
    }
}
