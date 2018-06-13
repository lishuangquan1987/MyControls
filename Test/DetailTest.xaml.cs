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
using System.Windows.Shapes;
using System.Threading;
using TemperatureGuage;
using CircleProcessBar;

namespace Test
{
    /// <summary>
    /// DetailTest.xaml 的交互逻辑
    /// </summary>
    public partial class DetailTest : Window
    {
        public DetailTest()
        {
            InitializeComponent();
        }
        Random r = new Random();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(x =>
                {
                    while (true)
                    {
                        Thread.Sleep(2000);

                        this.Dispatcher.Invoke((Action)(() =>
                            {

                                TemperatureGuage.TemperatureGuageControl control = this.temperatureGrid.Children[r.Next(0, 3)] as TemperatureGuageControl;
                                if (control != null)
                                    control.CurrentValue = r.NextDouble() * (control.MaxValue - control.MinHeight);

                                CircleProcessBar.CircularProcessBarControl control1 = this.processBarGrid.Children[r.Next(0, 3)] as CircularProcessBarControl;
                                if (control1 != null)
                                {
                                    control1.SuccessRate = r.Next(0, 101);
                                }
                            }));
                    }
                });
        }
    }
}
