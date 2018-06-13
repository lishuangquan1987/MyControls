using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace TonyTab
{
    
   public class TonyTabItem:TabItem
    {
       public Label lb = null;
      
       public static readonly DependencyProperty SelectedColorProperty =
          DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(TonyTabItem),
          new PropertyMetadata(new SolidColorBrush(Colors.Blue), null));
       /// <summary>
       /// 选中Tab后的颜色
       /// </summary>
       public Brush SelectedColor
       {
           get { return (Brush)GetValue(SelectedColorProperty); }
           set { SetValue(SelectedColorProperty, value); }
       }

       public static readonly DependencyProperty UnderLineHeightProperty =
           DependencyProperty.Register("UnderLineHeight", typeof(double), typeof(TonyTabItem),
           new PropertyMetadata((double)5, null));
      /// <summary>
      /// 底线的高度
      /// </summary>
       public double UnderLineHeight
       {
           get
           {
               return (double)GetValue(UnderLineHeightProperty);
           }
           set
           {
               SetValue(UnderLineHeightProperty, value);
           }
       }

       static TonyTabItem()
       {
           DefaultStyleKeyProperty.OverrideMetadata(typeof(TonyTabItem), new FrameworkPropertyMetadata(typeof(TonyTabItem)));
       }
       public TonyTabItem()
       {
           this.Loaded += MyTabItem_Loaded;
           this.Width = 50;
       }

       void MyTabItem_Loaded(object sender, System.Windows.RoutedEventArgs e)
       {
           this.OnApplyTemplate();
           
       }
       public override void OnApplyTemplate()
       {
           base.OnApplyTemplate();
           
           lb = GetTemplateChild("lb") as Label;
         

           TonyTabControl tc = this.Parent as TonyTabControl;
           if (tc != null && this.lb != null)
           {
               if (tc.SelectedItem == this)
               {
                   this.lb.Visibility = System.Windows.Visibility.Visible;
                   this.Foreground = SelectedColor;
               }
               else
               {
                   this.lb.Visibility = System.Windows.Visibility.Collapsed;
                   this.Foreground = new SolidColorBrush(Colors.Black);
               }
           }                           
       }
    }
}
