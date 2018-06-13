using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace CircleProcessBar
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CircleProcessBar"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CircleProcessBar;assembly=CircleProcessBar"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CircularProcessBarControl : UserControl, INotifyPropertyChanged
    {

        private DispatcherTimer timer;
        private int currentRate = 0;
        public CircularProcessBarControl()
        {
            this.DataContext = this;

            //timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            //timer.Tick += timer_Tick;
            //timer.Start();
        }

        #region Properties

        private int successRate = 100;
        public int SuccessRate
        {
            get
            {
                return successRate;
            }
            set
            {
                if (value != successRate)
                {
                    successRate = value;
                    OnPropertyChanged("SuccessRate");
                }
            }
        }

        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            currentRate++;
            SuccessRate = currentRate * 100 / 100;
            if (SuccessRate == 100)
            {
                timer.Stop();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        static CircularProcessBarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularProcessBarControl), new FrameworkPropertyMetadata(typeof(CircularProcessBarControl)));
        }
    }
   
}
