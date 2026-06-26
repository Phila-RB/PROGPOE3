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
            InitializeComponent();
            //DataTable dt = Program.ShowTasks();
            UpdateView();

            //foreach(DataColumn columns in dt.Columns)
            //{
            //    taskData.Columns.Add(new DataGridTextColumn
            //    {
            //        Header = columns.ColumnName,
            //        Binding = new Binding(columns.ColumnName)
            //    });
            //}
           
            //foreach (DataRowView row in dt.DefaultView)
            //{
            //    taskData.Items.Add(row);
            //}

        }

    

    public void UpdateView() {
            taskData.ItemsSource = Program.getData().DefaultView;
            DataGridTextColumn column = new();

            taskData.Columns.Add(column);
        }
    }
}
