using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace TonyTab2017
{
   internal class DoubleToMargin:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double d = (double)value;
            return new Thickness(d, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   internal class BoolToVisiable : IValueConverter
   {

       public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
           bool b = (bool)value;
           if (b)
               return System.Windows.Visibility.Visible;
           else
               return System.Windows.Visibility.Collapsed;
       }

       public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
           throw new NotImplementedException();
       }
   }
}
