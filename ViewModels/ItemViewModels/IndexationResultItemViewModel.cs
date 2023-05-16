using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.ViewModels.ItemViewModels
{
    public class IndexationResultItemViewModel
    {
        public string Word { get; }

        public List<string> FilesList { get; }

        public List<string> Numbers_Positions { get; }

        public IndexationResultItemViewModel(string word, List<string> filesList, List<string>  numbers_Positions)
        {
            Word = word;
            FilesList = filesList;
            FilesList.ForEach(delegate (string fileName)
            {
                fileName = Path.GetFileName(fileName);
            });
            Numbers_Positions = numbers_Positions;
        }
    }
}
