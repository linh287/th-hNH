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
using System.Windows.Shapes;

namespace RevitAddIn1.Bai5EdittingCreating.CreateSheet.View
{
    /// <summary>
    /// Interaction logic for RenameSheetView.xaml
    /// </summary>
    public partial class CreateSheetView : Window
    {
        public CreateSheetView()
        {
            InitializeComponent();
        }

        public object SheetDataGrid { get; internal set; }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
