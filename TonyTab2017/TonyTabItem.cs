using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace TonyTab2017
{
   [ContentProperty("ItemContent")]
   public class TonyTabItem:UserControl
    {
       static TonyTabItem()
       {
           //当此依赖项属性位于指定类型的实例上时为其指定替换元数据，以在该依赖项属性继承自基类型时重写该属性已存在的元数据。
           DefaultStyleKeyProperty.OverrideMetadata(typeof(TonyTabItem), new FrameworkPropertyMetadata(typeof(TonyTabItem)));
       }
      

       
       #region 字段
       private Grid mainGrid = null;
       private Label lbHeader = null;
       private TonyTabItem oldSelectedItem = null;
       #endregion

       #region 依赖属性
       public static readonly DependencyProperty HeaderWidthProperty =
           DependencyProperty.Register("HeaderWidth", typeof(double), typeof(TonyTabItem),
           new PropertyMetadata((double)150, OnHeaderSizeChanged));

       public static readonly DependencyProperty HeaderHeightProperty =
           DependencyProperty.Register("HeaderHeight", typeof(double), typeof(TonyTabItem),
           new PropertyMetadata((double)50, OnHeaderSizeChanged));

       public static readonly DependencyProperty HeaderLeftMarginProperty =
           DependencyProperty.Register("HeaderLeftMargin", typeof(double), typeof(TonyTabItem),
           new PropertyMetadata((double)5, OnHeaderSizeChanged));

       public static readonly DependencyProperty HeaderTextProperty =
          DependencyProperty.Register("HeaderText", typeof(string), typeof(TonyTabItem),
          new PropertyMetadata("TonyTab2017", null));

       public static readonly DependencyProperty HeaderFontSizeProperty =
           DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(TonyTabItem),
           new PropertyMetadata((double)20, null));


       public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected", typeof(bool), typeof(TonyTabItem),
          new PropertyMetadata(false, OnSelectedChanged));

       public static readonly DependencyProperty ItemContentProperty =
           DependencyProperty.Register("ItemContent", typeof(object), typeof(TonyTabItem),
           new PropertyMetadata(null));




       public static readonly DependencyProperty HeaderBackgroundProperty =
           DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(TonyTabItem),
           new PropertyMetadata(new SolidColorBrush(Colors.Transparent), null));

       public static readonly DependencyProperty HeaderBackground_MouseOverProperty =
           DependencyProperty.Register("HeaderBackground_MouseOver", typeof(Brush), typeof(TonyTabItem),
           new PropertyMetadata(new SolidColorBrush(Colors.LightBlue), null));

       public static readonly DependencyProperty HeaderBackground_SelectedProperty =
           DependencyProperty.Register("HeaderBackground_Selected", typeof(Brush), typeof(TonyTabItem),
           new PropertyMetadata(new SolidColorBrush(Colors.Blue), null));




       public static readonly DependencyProperty HeaderForegroundProperty =
           DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(TonyTabItem),
           new PropertyMetadata(new SolidColorBrush(Colors.Black), null));

       public static readonly DependencyProperty HeaderForeground_MouseOverProperty =
           DependencyProperty.Register("HeaderForeground_MouseOver", typeof(Brush), typeof(TonyTabItem),
           new PropertyMetadata(new SolidColorBrush(Colors.Black), null));

       public static readonly DependencyProperty HeaderForeground_SelectedProperty =
           DependencyProperty.Register("HeaderForeground_Selected", typeof(Brush), typeof(TonyTabItem),
           new PropertyMetadata(new SolidColorBrush(Colors.White), null));

       #endregion

       #region 属性
       public double HeaderWidth
       {
           get { return (double)GetValue(HeaderWidthProperty); }
           set { SetValue(HeaderWidthProperty, value); }
       }
       public double HeaderHeight
       {
           get { return (double)GetValue(HeaderHeightProperty); }
           set { SetValue(HeaderHeightProperty, value); }
       }
       public double HeaderLeftMargin
       {
           get { return (double)GetValue(HeaderLeftMarginProperty); }
           set { SetValue(HeaderLeftMarginProperty, value); }
       }
       public string HeaderText
       {
           get { return (string)GetValue(HeaderTextProperty); }
           set { SetValue(HeaderTextProperty, value); }
       }

       public double HeaderFontSize
       {
           get { return (double)GetValue(HeaderFontSizeProperty); }
           set { SetValue(HeaderFontSizeProperty, value); }
       }

       private double headerLeftMarginEx = 5;
       public double HeaderLeftMarginEx
       {
           get { return headerLeftMarginEx; }
           set { headerLeftMarginEx = value; }
       }

       public bool IsSelected
       {
           get { return (bool)GetValue(IsSelectedProperty); }
           set { SetValue(IsSelectedProperty, value); }
       }
       public object ItemContent
       {
           get { return GetValue(ItemContentProperty); }
           set { SetValue(ItemContentProperty, value); }
       }

       #region Header的背景色及前景色
       public Brush HeaderBackground
       {
           get { return (Brush)GetValue(HeaderBackgroundProperty); }
           set { SetValue(HeaderBackgroundProperty, value); }
       }

       public Brush HeaderBackground_MouseOver
       {
           get { return (Brush)GetValue(HeaderBackground_MouseOverProperty); }
           set { SetValue(HeaderBackground_MouseOverProperty, value); }
       }
       public Brush HeaderBackground_Selected
       {
           get { return (Brush)GetValue(HeaderBackground_SelectedProperty); }
           set { SetValue(HeaderBackground_SelectedProperty, value); }
       }
       public Brush HeaderForeground
       {
           get { return (Brush)GetValue(HeaderForegroundProperty); }
           set { SetValue(HeaderForegroundProperty, value); }
       }
       public Brush HeaderForeground_MouseOver
       {
           get { return (Brush)GetValue(HeaderForeground_MouseOverProperty); }
           set { SetValue(HeaderForeground_MouseOverProperty, value); }
       }
       public Brush HeaderForeground_Selected
       {
           get { return (Brush)GetValue(HeaderForeground_SelectedProperty); }
           set { SetValue(HeaderForeground_SelectedProperty, value); }
       }
       #endregion
       #endregion

       #region 内部事件
       internal event Action<SelectionChangedCancelEventArgs> ItemSelected;
       #endregion

       #region 方法
       private static void OnHeaderSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
       {          
           
           TonyTabItem tonyTabItem = d as TonyTabItem;
           TonyTabControl tonyTabControl = tonyTabItem.Parent as TonyTabControl;
           if (tonyTabControl == null)
               return;
           if (tonyTabItem.mainGrid == null)
               return;
           if (tonyTabItem.lbHeader == null)
               return;
           //处理leftMargin和HeaderHeight
           List<TonyTabItem> items = new List<TonyTabItem>();
           foreach (var item in tonyTabControl.Items)
           {
               items.Add(item as TonyTabItem);
           }
           //找出最大的HeaderHeight
           double headerHeightMax = (from i in items select i.HeaderHeight).Max();
           //都设置为最大的headerHeightMax
           items.ForEach(x => x.HeaderHeight = headerHeightMax);
           //处理leftMargin:TonyTabItem中的Leftmargin是Header和左边的距离，现在改为Header与Header的距离
           double leftMargin = 0;
           foreach (TonyTabItem item in items)
           {
               leftMargin += item.HeaderLeftMargin;
               //item.HeaderLeftMarginEx = leftMargin;
               if (item == tonyTabItem)
                   break;
               leftMargin += item.HeaderWidth;
               
           }
           //获取LeftMargin:Header距离最左边的距离
           tonyTabItem.HeaderLeftMarginEx = leftMargin;
           //设置Header的Margin
           tonyTabItem.lbHeader.Margin = new Thickness(leftMargin, 0, 0, 0);

           //处理剪切
           double left = tonyTabItem.HeaderLeftMarginEx;
           double h = tonyTabItem.HeaderHeight;
           double w = tonyTabItem.HeaderWidth;

           double ah = tonyTabControl.ActualHeight;
           double aw = tonyTabControl.ActualWidth;
           //Header 有4个点，ContentPresenter有各点，按点裁剪
           Point p1 = new Point(left, 0);
           Point p2 = new Point(left + w, 0);
           Point p3 = new Point(left + w, h);
           Point p4 = new Point(aw, h);
           Point p5 = new Point(aw, ah);
           Point p6 = new Point(0, ah);
           Point p7 = new Point(0, h);
           Point p8 = new Point(left, h);

           StreamGeometry geometry = new StreamGeometry();
           using (StreamGeometryContext ctx = geometry.Open())
           {
               ctx.BeginFigure(p1, true, true);
               ctx.LineTo(p2, true, true);
               ctx.LineTo(p3, true, true);
               ctx.LineTo(p4, true, true);
               ctx.LineTo(p5, true, true);
               ctx.LineTo(p6, true, true);
               ctx.LineTo(p7, true, true);
               ctx.LineTo(p8, true, true);
           }
           tonyTabItem.mainGrid.Clip = geometry;//按照Header进行裁剪

       }
       private static void OnSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
       {

           TonyTabItem tonyTabItem = d as TonyTabItem;
           //激发SelectionChanged事件,然后TabControl中处理
           if (tonyTabItem.ItemSelected != null&&(bool)e.NewValue)
           {

               tonyTabItem.ItemSelected(new SelectionChangedCancelEventArgs(tonyTabItem.oldSelectedItem, tonyTabItem));
           }
           
       }

       public override void OnApplyTemplate()
       {
           base.OnApplyTemplate();
           mainGrid = GetTemplateChild("mainGrid") as Grid;
           lbHeader = GetTemplateChild("lbHeader") as Label;
       }
       public TonyTabItem()
       {
           this.DataContext = this;
           this.Loaded += TonyTabItem_Loaded;
       }
       void TonyTabItem_Loaded(object sender, RoutedEventArgs e)
       {
           this.OnApplyTemplate();
           if(lbHeader!=null)
               lbHeader.MouseLeftButtonDown += lbHeader_MouseLeftButtonDown;
           OnHeaderSizeChanged(this, new DependencyPropertyChangedEventArgs());
           //注册事件,在父控件中响应
           TonyTabControl parent = this.Parent as TonyTabControl;
           if (parent != null)
           {
               this.ItemSelected += parent.tonyTabItem_Selected;
           }
       }

       void lbHeader_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
       {
           e.Handled = true;
           if (this.IsSelected)
               return;
           TonyTabControl parent = this.Parent as TonyTabControl;          
           
           if (parent != null && parent.Items.Count > 0)
           {
               foreach (var item in parent.Items)
               {
                   TonyTabItem tonyTabItem = item as TonyTabItem;
                   if (item == null)
                       throw new Exception("非TonyTabItem");
                   //OldItem方面待后续处理，现在只能传入当前选中的Item
                   //if (tonyTabItem.IsSelected)
                   //    oldSelectedItem = tonyTabItem;//获取以前被选中的Item
                   if (tonyTabItem != null)
                   {
                       if (tonyTabItem == this)
                           tonyTabItem.IsSelected = true;
                       else
                           tonyTabItem.IsSelected = false;
                   }
               }
           }
           
       }
        #endregion
    }
}
