using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace Prog
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataViewer : Window
    {
        public DataViewer()
        {
            InitializeComponent(); //create new data view window 
            UpdateView();

        }
    public void UpdateView() {//update data grid with new data
            taskData.ItemsSource = Program.getData().DefaultView;
            DataGridTextColumn column = new();

            taskData.Columns.Add(column);
        }
    }
}
