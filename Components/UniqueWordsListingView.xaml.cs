﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IndexApp.Components
{
    /// <summary>
    /// Логика взаимодействия для UniqueWordsListingView.xaml
    /// </summary>
    public partial class UniqueWordsListingView : UserControl
    {
        public UniqueWordsListingView()
        {
            InitializeComponent();
        }

        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}