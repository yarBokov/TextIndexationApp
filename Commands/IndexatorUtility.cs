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

        private static List<char> separators = new List<char> { ' ', ':', ';', '.', ',', '!', '?' };

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
            //foreach (var line in BaseLines)
            //{
            //    lineBuf = line.Trim(separators.ToArray());
            //    foreach (char ch in separators)
            //    {
            //        lineBuf = Regex.Replace(lineBuf, @"\" + ch.ToString() + "+", ch.ToString());
            //        lineBuf = Regex.Replace(lineBuf, @"\" + ch.ToString() + @"\s", ch.ToString());
            //    }
            //    UniqueWords.UnionWith(lineBuf.Split(separators.ToArray()));
            //}
            foreach (var line in BaseLines)
            {
                lineBuf = new string((from c in line
                    where char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || separators.Contains(c)
                    select c
                    ).ToArray());
                lineBuf = lineBuf.Trim(separators.ToArray());
                foreach (char ch in separators)
                {
                    lineBuf = Regex.Replace(lineBuf, @"\" + ch.ToString() + "+", ch.ToString());
                    lineBuf = Regex.Replace(lineBuf, @"\" + ch.ToString() + @"\s", ch.ToString());
                }
                UniqueWords.UnionWith(lineBuf.Split(separators.ToArray()));
            }
        }

        public List<Tuple<string, List<string>, List<string> > > getResults()
        {
            List<Tuple<string, List<string>, List<string>>> tuples = new List<Tuple<string, List<string>, List<string>>>();
            foreach (string word in UniqueWords)
            {
                var wordFileList = getFilesPerWord(word);
                var numbersPositions = findNumberPos(wordFileList, word);
                try
                {
                    tuples.Add(Tuple.Create(word, wordFileList, numbersPositions));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return tuples;
        }

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
            int lineNumber = 0;
            foreach (string file in files)
            {
                foreach (string line in File.ReadLines(file))
                {
                    List<int> indexesList = new List<int>();
                    lineNumber++;
                    try
                    {
                        indexesList = allIndexesOf(line, word);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (!indexesList.Any())
                        continue;
                    numbersPositions.Add("{" + lineNumber.ToString() + "}" + " - [" + string.Join(", ", indexesList.ToArray()) + "]");
                }
            }
            return numbersPositions;
        }

        private static List<int> allIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("искомое слово не может быть пустым", "value");
            List<int> indexes = new List<int>();
            List<char> checkChars = new List<char>(separators);
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                if ((index + value.Length < str.Length && char.IsLetterOrDigit(str[index + value.Length])) || 
                    (index > 0 && char.IsLetterOrDigit(str[index - 1])))
                    continue;
                indexes.Add(index + 1);
            }
        }

    }
}
