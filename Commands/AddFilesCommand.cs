using IndexApp.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace IndexApp.Commands
{
    public class AddFilesCommand : CommandBase
    {
        private readonly FilesListingViewModel _filesListingViewModel;
        private OpenFileDialog openFileDialog;

        public AddFilesCommand(FilesListingViewModel filesListingViewModel)
        {
            _filesListingViewModel = filesListingViewModel;
            openFileDialog = new OpenFileDialog() 
            { 
                Multiselect= true,
                Filter = "txt files(*.txt) | *.txt",
                InitialDirectory = "",
                Title = "Открыть файлы",
                DefaultExt = ".txt"
            };
        }

        public override void Execute(object? parameter)
        {
            List<string> files = new List<string>();
            if (openFileDialog.ShowDialog() == true)
            {
                files = getFileNames(openFileDialog.FileNames).OrderBy(i => i).ToList();
            }
            foreach (string fileName in files)
            {
                _filesListingViewModel.AddItem(fileName);
            }
        }

        private List<string> getFileNames(string[] files)
        {
            List<string> fileNames = new List<string>();
            foreach (string filepath in files)
            {
                fileNames.Add(filepath);
            }
            return fileNames;
        }
    }
}
