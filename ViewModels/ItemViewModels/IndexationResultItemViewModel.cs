﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexApp.ViewModels.ItemViewModels
{
    public class IndexationResultItemViewModel : ViewModelBase
    {
        public string Word { get; }

        public List<string> FilesList { get; }

        public List<string> Numbers_Positions { get; }

        public IndexationResultItemViewModel(string word, List<string> filesList, List<string> numbers_Positions)
        {
            Word = word;
            FilesList = filesList.Select(file => new DirectoryInfo(file).Parent.Name + "/" + Path.GetFileName(file)).ToList();
            Numbers_Positions = numbers_Positions;
        }
    }
}
