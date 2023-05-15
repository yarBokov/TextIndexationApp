using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.ViewModels.ItemViewModels
{
    public class UniqueWordsItemViewModel : ViewModelBase
    {
        public string Word { get; }

        public UniqueWordsItemViewModel(string word)
        {
            Word = word;
        }
    }
}
