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

        private static char[] separators = new char[] { ' ', ':', ';', '.', ',' };

        public IndexatorUtility(FilesListingViewModel viewModel)
        {
            _viewModel = viewModel;
            fileNames = _viewModel.getFullPathFileNames();
            UniqueWords = new SortedSet<string>();
            BaseLines = new List<string>();
            getAllLinesFromFiles();
            getWords();
        }

        private void getAllLinesFromFiles()
        {
            List<string> missingFiles = new List<string>();
            foreach (var file in fileNames)
            {
                if (File.Exists(file))
                {
                    BaseLines.AddRange(File.ReadLines(file));
                }
                else
                {
                    missingFiles.Add(Path.GetFileName(file));
                    _viewModel.RemoveItem(Path.GetFileName(file), file);
                }
            }
            if (missingFiles.Any())
            {
                MessageBox.Show($"{string.Join(',', missingFiles.ToArray())} {((missingFiles.Count == 1) ? "не был найден" : "не были найдены")}",
                    "Предупрежедение!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void getWords()
        {
            string lineBuf = string.Empty;
            foreach (var line in BaseLines)
            {
                lineBuf = line.Trim(separators);
                foreach (char ch in separators)
                {
                    lineBuf = Regex.Replace(lineBuf, @"\" + ch.ToString() + "+", ch.ToString());
                    lineBuf = Regex.Replace(lineBuf, @"\" + ch.ToString() + @"\s", ch.ToString());
                }
                UniqueWords.UnionWith(lineBuf.Split(separators));
            }
        }

        public List<Tuple<string, List<string>, List<string> > > getResults()
        {
            List<Tuple<string, List<string>, List<string>>> tuples = new List<Tuple<string, List<string>, List<string>>>();
            foreach (string word in UniqueWords)
            {
                var wordFileList = getFilesPerWord(word);
                try
                {
                    tuples.Add(Tuple.Create(word, wordFileList, findNumberPos(wordFileList, word)));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return tuples;
        }

        //public List<Tuple<string, string, int, int>> getFileWordPositions()
        //{
        //    List<Tuple<string, string, int, int>> listTuple = new List<Tuple<string, string, int, int>>();
        //    foreach (string file in fileNames)
        //    {

        //        listTuple.Add(Tuple.Create(file, ));
        //    }
        //    return listTuple;
        //}

        private List<string> getFilesPerWord(string word)
        {
            List<string> listFiles = new List<string>();
            foreach (string file in fileNames)
            {
                if (File.ReadAllText(file).Contains(word))
                    listFiles.Add(file);
            }
            return listFiles;
        }

        private List<string> findNumberPos(List<string> files, string word)
        {
            List<string> numbersPositions = new List<string>();
            int lineNumber;
            foreach (string file in files)
            {
                foreach (string line in File.ReadLines(file))
                {
                    lineNumber = 1;
                    var indexesList = allIndexesOf(line, word);
                    if (!indexesList.Any())
                    {
                        lineNumber++;
                        continue;
                    }
                    numbersPositions.Add(lineNumber.ToString() + " - [" + string.Join(", ", indexesList.ToArray()) + "]");
                    lineNumber++;
                }
            }
            return numbersPositions;
        }

        private List<int> allIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("искомое слово не может быть пустым", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index) + 1;
                if (index == 0)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}
