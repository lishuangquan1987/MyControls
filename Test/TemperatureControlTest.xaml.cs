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

namespace Test
{
    /// <summary>
    /// TemperatureControlTest.xaml 的交互逻辑
    /// </summary>
    public partial class TemperatureControlTest : Window
    {
        public TemperatureControlTest()
        {
            InitializeComponent();
        }
        int ErrorCount = 0;
        double MaxValue = 0;
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ErrorCount = 0;
            MaxValue = double.Parse(this.tbMaxRange.Text);
            new System.Threading.Thread(Start).Start();
        }
        Random random = new Random();
        private void Start()
        {
            for (int i = 0; i < 99; i++)
            {
                System.Threading.Thread.Sleep(100);
                double value= random.NextDouble() * 100;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.pb.Value = (i + 2);
                    this.tg.CurrentValue = value;
                    if (value > MaxValue)
                    {
                        ErrorCount++;
                        this.tbErrorCount.Text = ErrorCount.ToString();
                    }
                }));
            }
        }
    }
}
