using IndexApp.ViewModels.ItemViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.ViewModels
{
    public class IndexationResultViewModel : ViewModelBase
    {
        private readonly ObservableCollection<IndexationResultItemViewModel> _indexationResultModels;

        public IEnumerable<IndexationResultItemViewModel> IndexationResults => _indexationResultModels;

        private IndexationResultItemViewModel _item;

        public IndexationResultItemViewModel Item
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

        public IndexationResultViewModel()
        {
            _indexationResultModels = new ObservableCollection<IndexationResultItemViewModel>();
        }

        public void InsertResults(List< Tuple < string, List<string>, string > > tuples)
        {
            foreach (var tuple in tuples)
            {
                Item = new IndexationResultItemViewModel(tuple.Item1, tuple.Item2, tuple.Item3);
                _indexationResultModels.Add(Item);
            }
        }

        public void Clear()
        {
            _indexationResultModels.Clear();
        }
    }
}
