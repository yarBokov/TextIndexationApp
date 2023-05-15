using IndexApp.ViewModels;
using IndexApp.ViewModels.ItemViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace IndexApp.Commands
{
    public class IndexatorUtility
    {

        private readonly FilesListingViewModel _viewModel;
        public SortedSet<string> UniqueWords { get; set; }

        private List<string> BaseLines { get; set; }

        private List<string> fileNames;

        private static char[] separators = new char[]{ ',', ':', ';', '.', ' ' };

        public IndexatorUtility(FilesListingViewModel viewModel)
        {
            _viewModel = viewModel;
            fileNames = _viewModel.getFullPathFileNames().OrderBy(i => i).ToList();
            UniqueWords = new SortedSet<string>();
            BaseLines = new List<string>();
            getAllLinesFromFiles();
            getWords();
        }

        private void getAllLinesFromFiles()
        {
            List<string> missingFiles = new List<string>();
            foreach(var file in fileNames)
            {
                if (File.Exists(file))
                    BaseLines.AddRange(File.ReadLines(file));
                else
                {
                    missingFiles.Add(Path.GetFileName(file));
                    _viewModel.RemoveItem(Path.GetFileName(file), file);
                }
            }
            if (missingFiles.Any())
            {
                MessageBox.Show($"{string.Join(',', missingFiles).ToArray()} {((missingFiles.Count == 1) ? "не был найден" : "не были найдены")}",
                    "Предупрежедение!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void getWords()
        {
            string lineBuf = string.Empty;
            foreach(var line in BaseLines)
            {
                lineBuf = line.Replace("...", ".");
                foreach(char ch in separators)
                {
                    lineBuf = Regex.Replace(lineBuf, " *, *", ch.ToString());
                }
                UniqueWords.UnionWith(lineBuf.Split(separators));
            }
        }
    }
}
