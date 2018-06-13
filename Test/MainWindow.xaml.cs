using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //waitControl.IsActive = !waitControl.IsActive;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                for (int i = 0; i < 100; i++)
                {
                    this.Dispatcher.Invoke((Action)(() => { this.circleBar.SuccessRate = i; }));
                    Thread.Sleep(500);
                }
            });
        }
    }
}
