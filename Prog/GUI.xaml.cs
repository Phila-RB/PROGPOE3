using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Prog
{
    /// <summary>
    /// Interaction logic for GUI.xaml
    /// </summary>
    public partial class GUI : Window
    {
        public TaskCompletionSource<string> finTask;
        public event Action<string> MsgSent;
        public GUI()
        {
            //starts window app
            InitializeComponent();
            DataContext = this;
        }

        //sends message
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            finTask?.TrySetResult(msgBox.Text.ToLower());//completes set task
            if (string.IsNullOrEmpty(msgBox.Text))
            {
                return;
            }
            else
            {
                //displays sent message
                TextBlock newMsg = new TextBlock();
                newMsg.TextWrapping = TextWrapping.Wrap;
                newMsg.Margin = new Thickness(10);
                newMsg.Text = msgBox.Text;
                msgView.Children.Add(newMsg);

                MsgSent?.Invoke(msgBox.Text);

                msgBox.Clear();

            }

        }

        //async function to wait for msg answers
        public Task<String> RunConversationAsync()
        {
            finTask = new TaskCompletionSource<string>();
            return finTask.Task;
        }

    }
}
