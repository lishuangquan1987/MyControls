/////
////Author :李双全
////Date:20170516
////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.ComponentModel;


namespace TemperatureGuage
{
   /// <summary>
   /// 经验:能用绑定解决的，绝不用写代码去实现，
   /// 除了需要播放动画和直接绑定解决不了才用一个回调函数去实现
   /// </summary>
   [TemplatePart(Name = "rect", Type = typeof(Rectangle))]
   [TemplatePart(Name = "rootCanvas", Type = typeof(Canvas))]
   [TemplatePart(Name = "lbMsg", Type = typeof(Label))]
   public class TemperatureGuageControl:UserControl
   {
       #region Private variables
       private Rectangle rect;//显示温度计的值
       private Canvas rootCanvas;
       private Label lbMsg;
       #endregion

       #region 分组常量
       private const string mainScaleInfo = "主刻度设置";
       private const string minorScaleInfo = "次刻度设置";
       private const string valueInfo = "值设置";
       private const string nameInfo = "温度计名称相关信息设置";
       private const string meterInfo = "温度计的位置和大小设置";
       #endregion

       #region 构造函数及其初始化

       static  TemperatureGuageControl()
       {
           DefaultStyleKeyProperty.OverrideMetadata(typeof(TemperatureGuageControl), new FrameworkPropertyMetadata(typeof(TemperatureGuageControl)));
       }
       public TemperatureGuageControl()
       {
           this.Loaded += TemperatureGuageControl_Loaded;
       }

       void TemperatureGuageControl_Loaded(object sender, RoutedEventArgs e)
       {
           this.OnApplyTemplate();
           CurrentValueChanged(new DependencyPropertyChangedEventArgs(TemperatureGuageControl.CurrentValueProperty, 0, 0));
       }
       #endregion

       #region 依赖属性
       public static readonly DependencyProperty ColorProperty =
          DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(TemperatureGuageControl),
          new PropertyMetadata(new SolidColorBrush(Colors.Green), null));

       public static readonly DependencyProperty MinValueProperty =
           DependencyProperty.Register("MinValue", typeof(double), typeof(TemperatureGuageControl),
           new PropertyMetadata((double)0, new PropertyChangedCallback(TemperatureGuageControl.OnAllChanged)));

       
       public static readonly DependencyProperty MaxValueProperty =
           DependencyProperty.Register("MaxValue", typeof(double), typeof(TemperatureGuageControl),
           new PropertyMetadata((double)100, new PropertyChangedCallback(TemperatureGuageControl.OnAllChanged)));

       public static readonly DependencyProperty CurrentValueProperty =
          DependencyProperty.Register("CurrentValue", typeof(double), typeof(TemperatureGuageControl),
          new PropertyMetadata((double)50, new PropertyChangedCallback(TemperatureGuageControl.OnValuePropertyChanged)));

       public static readonly DependencyProperty MeterHeightProperty =
         DependencyProperty.Register("MeterHeight", typeof(double), typeof(TemperatureGuageControl),
         new PropertyMetadata((double)100, new PropertyChangedCallback(TemperatureGuageControl.OnAllChanged)));

       public static readonly DependencyProperty MeterWidthProperty =
        DependencyProperty.Register("MeterWidth", typeof(double), typeof(TemperatureGuageControl),
        new PropertyMetadata((double)20, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));

       public static readonly DependencyProperty MeterMarginProperty =
       DependencyProperty.Register("MeterMargin", typeof(Thickness), typeof(TemperatureGuageControl),
       new PropertyMetadata(new Thickness(0, -74, 0, 0), new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));



       public static readonly DependencyProperty MainScaleCountProperty =
       DependencyProperty.Register("MainScaleCount", typeof(int), typeof(TemperatureGuageControl),
       new PropertyMetadata(5, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));

       public static readonly DependencyProperty MainScaleHeightProperty =
       DependencyProperty.Register("MainScaleHeight", typeof(double), typeof(TemperatureGuageControl),
       new PropertyMetadata((double)2, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));

       public static readonly DependencyProperty MainScaleWidthProperty =
          DependencyProperty.Register("MainScaleWidth", typeof(double), typeof(TemperatureGuageControl),
          new PropertyMetadata((double)10, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));

       public static readonly DependencyProperty MainScaleColorProperty =
          DependencyProperty.Register("MainScaleColor", typeof(Color), typeof(TemperatureGuageControl),
          new PropertyMetadata(Colors.Black, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));




       public static readonly DependencyProperty MinorScaleCountProperty =
          DependencyProperty.Register("MinorScaleCount", typeof(int), typeof(TemperatureGuageControl),
          new PropertyMetadata(3, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));

       public static readonly DependencyProperty MinorScaleHeightProperty =
         DependencyProperty.Register("MinorScaleHeight", typeof(double), typeof(TemperatureGuageControl),
         new PropertyMetadata((double)1, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));

       public static readonly DependencyProperty MinorScaleWidthProperty =
         DependencyProperty.Register("MinorScaleWidth", typeof(double), typeof(TemperatureGuageControl),
         new PropertyMetadata((double)5, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));


       public static readonly DependencyProperty MinorScaleColorProperty =
         DependencyProperty.Register("MinorScaleColor", typeof(Color), typeof(TemperatureGuageControl),
         new PropertyMetadata(Colors.Green, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));




       public static readonly DependencyProperty ScaleLabelForgoundProperty =
        DependencyProperty.Register("ScaleLabelForgound", typeof(Color), typeof(TemperatureGuageControl),
        new PropertyMetadata(Colors.Green, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));

       public static readonly DependencyProperty ScaleLabelFontSizeProperty =
        DependencyProperty.Register("ScaleLabelFontSize", typeof(double), typeof(TemperatureGuageControl),
        new PropertyMetadata((double)12, new PropertyChangedCallback(TemperatureGuageControl.OnScalePropertyChanged)));




       public static readonly DependencyProperty MsgLabelMargionProperty =
        DependencyProperty.Register("MsgLabelMargion", typeof(Thickness), typeof(TemperatureGuageControl),
        new PropertyMetadata(new Thickness(0,0,0,0), null));

       public static readonly DependencyProperty MsgLabelFontSizeProperty =
       DependencyProperty.Register("MsgLabelFontSize", typeof(double), typeof(TemperatureGuageControl),
       new PropertyMetadata((double)20, null));

       public static readonly DependencyProperty MsgProperty =
      DependencyProperty.Register("Msg", typeof(string), typeof(TemperatureGuageControl),
      new PropertyMetadata("Msg", null));

       public static readonly DependencyProperty MsgLableForegroundProperty =
     DependencyProperty.Register("MsgLableForeground", typeof(SolidColorBrush), typeof(TemperatureGuageControl),
     new PropertyMetadata(new SolidColorBrush(Colors.Black), null));
       #endregion

       #region 属性
       #region 基本值
       //温度计进度条显示的颜色
       [Description("设置进度条的颜色"), Category(TemperatureGuageControl.valueInfo)]
       public SolidColorBrush Color
       {
           get { return (SolidColorBrush)GetValue(ColorProperty); }
           set { SetValue(ColorProperty, value); }
       }
       [Description("设置温度计的最大值"), Category(TemperatureGuageControl.valueInfo)]
       public double MaxValue
       {
           get { return (double)GetValue(MaxValueProperty); }
           set { SetValue(MaxValueProperty, value); }
       }
       [Description("设置温度计的最小值"), Category(TemperatureGuageControl.valueInfo)]
       public double MinValue
       {
           get { return (double)GetValue(MinValueProperty); }
           set { SetValue(MinValueProperty, value); }
       }
       [Description("设置温度计的当前值"), Category(TemperatureGuageControl.valueInfo)]
       public double CurrentValue
       {
           get { return (double)GetValue(CurrentValueProperty); }
           set { SetValue(CurrentValueProperty, value); }
       }
       /// <summary>
       /// 温度计的高
       /// </summary>
       [Description("设置进度条的高度"), Category(TemperatureGuageControl.meterInfo)]
       public double MeterHeight
       {
           get { return (double)GetValue(MeterHeightProperty); }
           set { SetValue(MeterHeightProperty, value); }
       }
       /// <summary>
       /// 温度计的宽
       /// </summary>
       [Description("设置进度条的宽度"), Category(TemperatureGuageControl.meterInfo)]
       public double MeterWidth
       {
           get { return (double)GetValue(MeterWidthProperty); }
           set { SetValue(MeterWidthProperty, value); }
       }
       [Description("设置温度计在控件中的位置"), Category(TemperatureGuageControl.meterInfo)]
       public Thickness MeterMargin
       {
           get { return (Thickness)GetValue(MeterMarginProperty); }
           set { SetValue(MeterMarginProperty, value); }
       }
       #endregion

       #region 主刻度
       /// <summary>
       /// 主刻度的个数
       /// </summary>
       [Description("设置主刻度个数"), Category(TemperatureGuageControl.mainScaleInfo)]
       public int MainScaleCount
       {
           get { return (int)GetValue(MainScaleCountProperty); }
           set { SetValue(MainScaleCountProperty, value); }
       }
       /// <summary>
       /// 主刻度的高度
       /// </summary>
       [Description("设置主刻度高度"), Category(TemperatureGuageControl.mainScaleInfo)]
       public double MainScaleHeight
       {
           get { return (double)GetValue(MainScaleHeightProperty); }
           set { SetValue(MainScaleHeightProperty, value); }
       }
       /// <summary>
       /// 主刻度的宽度
       /// </summary>
       [Description("设置主刻度宽度"), Category(TemperatureGuageControl.mainScaleInfo)]
       public double MainScaleWidth
       {
           get { return (double)GetValue(MainScaleWidthProperty); }
           set { SetValue(MainScaleWidthProperty, value); }
       }
       /// <summary>
       /// 主刻度颜色
       /// </summary>
       [Description("设置主刻度颜色"), Category(TemperatureGuageControl.mainScaleInfo)]
       public Color MainScaleColor
       {
           get { return (Color)GetValue(MainScaleColorProperty); }
           set { SetValue(MainScaleColorProperty, value); }
       }
       #endregion

       #region 次刻度
       /// <summary>
       /// 相邻主刻度之间的次刻度的个数
       /// </summary>
       [Description("设置相邻主刻度之间的次刻度的个数"), Category(TemperatureGuageControl.minorScaleInfo)]
       public int MinorScaleCount
       {
           get { return (int)GetValue(MinorScaleCountProperty); }
           set { SetValue(MinorScaleCountProperty, value); }
       }
       /// <summary>
       /// 次刻度的高度
       /// </summary>
       [Description("设置次刻度的高度"), Category(TemperatureGuageControl.minorScaleInfo)]
       public double MinorScaleHeight
       {
           get { return (double)GetValue(MinorScaleHeightProperty); }
           set { SetValue(MinorScaleHeightProperty, value); }
       }
       /// <summary>
       /// 次刻度的宽度
       /// </summary>
       [Description("设置次刻度的宽度"), Category(TemperatureGuageControl.minorScaleInfo)]
       public double MinorScaleWidth
       {
           get { return (double)GetValue(MinorScaleWidthProperty); }
           set { SetValue(MinorScaleWidthProperty, value); }
       }
       /// <summary>
       /// 次刻度颜色 
       /// </summary>
       [Description("设置次刻度的颜色"), Category(TemperatureGuageControl.minorScaleInfo)]
       public Color MinorScaleColor
       {
           get { return (Color)GetValue(MinorScaleColorProperty); }
           set { SetValue(MinorScaleColorProperty, value); }
       }
       #endregion

       #region 显示主刻度值的Lable,次刻度没有Lable
       /// <summary>
       /// 刻度显示的刻度值字体颜色
       /// </summary>
       [Description("设置刻度显示的刻度值字体颜色"), Category(TemperatureGuageControl.mainScaleInfo)]
       public Color ScaleLabelForgound
       {
           get { return (Color)GetValue(ScaleLabelForgoundProperty); }
           set { SetValue(ScaleLabelForgoundProperty, value); }
       }
       /// <summary>
       /// 显示刻度值的字体大小
       /// </summary>
       [Description("设置刻度显示的刻度值字体大小"), Category(TemperatureGuageControl.mainScaleInfo)]
       public double ScaleLabelFontSize
       {
           get { return (double)GetValue(ScaleLabelFontSizeProperty); }
           set { SetValue(ScaleLabelFontSizeProperty, value); }
       }
       
       #endregion

       #region 显示温度计的名称
       /// <summary>
       /// 显示名称的Lable位置
       /// </summary>
        [Description("设置显示名称的Lable位置"), Category(TemperatureGuageControl.nameInfo)]
       public Thickness MsgLabelMargion
       {
           get { return (Thickness)GetValue(MsgLabelMargionProperty); }
           set { SetValue(MsgLabelMargionProperty, value); }
       }
       /// <summary>
       /// 显示名称的字体大小
       /// </summary>
       [Description("设置显示名称的字体大小"), Category(TemperatureGuageControl.nameInfo)]
       public double MsgLabelFontSize
       {
           get { return (double)GetValue(MsgLabelFontSizeProperty); }
           set { SetValue(MsgLabelFontSizeProperty, value); }
       }
       /// <summary>
       /// 温度计的名称
       /// </summary>
       [Description("设置显示名称"), Category(TemperatureGuageControl.nameInfo)]
       public string Msg
       {
           get { return (string)GetValue(MsgProperty); }
           set { SetValue(MsgProperty, value); }
       }
       [Description("设置显示名称的字体颜色"), Category(TemperatureGuageControl.nameInfo)]
       public SolidColorBrush MsgLableForeground
       {
           get { return (SolidColorBrush)GetValue(MsgLableForegroundProperty); }
           set { SetValue(MsgLableForegroundProperty, value); }
       }
       #endregion
       #endregion

       #region 方法
       public override void OnApplyTemplate()
       {
           base.OnApplyTemplate();
           rootCanvas = GetTemplateChild("rootCanvas") as Canvas;
           rect = GetTemplateChild("rect") as Rectangle;
           lbMsg = GetTemplateChild("lbMsg") as Label;
           if (rootCanvas == null || rect == null || lbMsg == null)
               return;
           DrawScale();
           Canvas.SetZIndex(rect, 10000);
       }
       //只更新值
       private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
       {
           TemperatureGuageControl tg = d as TemperatureGuageControl;          
           tg.CurrentValueChanged(e);
       }
       //只更新刻度
       private static void OnScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
       {
           TemperatureGuageControl tg = d as TemperatureGuageControl;
           tg.OnApplyTemplate();
       }
       //值和刻度都更新
       private static void OnAllChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
       {
           TemperatureGuageControl tg = d as TemperatureGuageControl;
           tg.OnApplyTemplate();
           tg.CurrentValueChanged(e);
       }
       /// <summary>
       /// 重新画刻度
       /// </summary>
       private void DrawScale()
       {
           //获取canvas
           rootCanvas = GetTemplateChild("rootCanvas") as Canvas;
           if (rootCanvas == null)
               return;
           //画刻度之前，清除原来的刻度
           List<UIElement> elements = new List<UIElement>();
           for (int i = 0; i < rootCanvas.Children.Count; i++)
           {
               UIElement u = rootCanvas.Children[i];
               if (u is Rectangle)
               {
                   if ((u as Rectangle).Name == "rect")
                       continue;
               }
               if (u is Label)
               {
                   if ((u as Label).Name == "lbMsg")
                       continue;
               }
               elements.Add(u);
           }
           elements.ForEach(x => rootCanvas.Children.Remove(x));

           rootCanvas.Height = MeterHeight;
           rootCanvas.Width = MeterWidth;
           //每一个主刻度的间隔长度
           double mainScaleLength=MeterHeight/(double)(MainScaleCount-1);
           //每相邻主刻度之间的差值
           double mainScaleValue=(MaxValue-MinValue)/(double)(MainScaleCount-1);

           //每一个次刻度的间隔长度.主刻度要-1，次刻度不用
           double minorScaleLength = mainScaleLength/(double)(MinorScaleCount+1);
           //double minorScaleValue = mainScaleValue / (double)(MinorScaleCount);

           //画主要刻度和次要刻度
           for (int i = 0; i <MainScaleCount; i++)
           {
               //画主刻度,刻度是有Rectangle组成，
               Rectangle mainScale = new Rectangle();
               mainScale.Height = MainScaleHeight;
               mainScale.Width = MainScaleWidth;
               mainScale.Fill =new SolidColorBrush(MainScaleColor);
               //设置主刻度的坐标
               Canvas.SetLeft(mainScale, MeterWidth);
               if (i == 0)
               {
                   Canvas.SetBottom(mainScale, 0);//第一个主刻度与底部对齐
               }
               else if (i == MainScaleCount - 1)
               {
                   Canvas.SetBottom(mainScale, MeterHeight-MainScaleHeight);//最后一个主刻度与最上面对齐
               }
               else
               {                  
                   Canvas.SetBottom(mainScale, i * mainScaleLength - MainScaleHeight / 2);
               }
                             
               rootCanvas.Children.Add(mainScale);

               //画主刻度的刻度值
               Label lable = new Label();//label的大小是自动的，随字体大小和内容长度变化               
               lable.FontSize = ScaleLabelFontSize;
               lable.FontWeight = FontWeights.Bold;
               lable.Foreground =new SolidColorBrush(ScaleLabelForgound);
               //Lable应该显示的值
               lable.Content = Math.Round(MinValue+mainScaleValue * i, 2).ToString();
               //设置显示刻度值Lable的坐标
               Canvas.SetLeft(lable, MeterWidth + MainScaleWidth + 1);
               //Canvas.SetBottom(lable, i * mainScaleLength - lable.ActualHeight / 2);
               Canvas.SetBottom(lable, i * mainScaleLength -ScaleLabelFontSize);
               rootCanvas.Children.Add(lable);

               //画次刻度
               if(i==MainScaleCount-1)
                   continue;
               for (int j = 0; j < MinorScaleCount; j++)
               {
                   Rectangle minorScale = new Rectangle();
                   minorScale.Fill =new SolidColorBrush(MinorScaleColor);
                   minorScale.Height = MinorScaleHeight;
                   minorScale.Width = MinorScaleWidth;
                   Canvas.SetLeft(minorScale, MeterWidth);
                   Canvas.SetBottom(minorScale, i * mainScaleLength + (j + 1) * minorScaleLength);
                   rootCanvas.Children.Add(minorScale);
               }
               
           }
       }
       private void CurrentValueChanged(DependencyPropertyChangedEventArgs e)
       {

           if (CurrentValue >= MaxValue)
               CurrentValue = MaxValue;
           if (CurrentValue <= MinValue)
               CurrentValue = MinValue;
           double h = (CurrentValue - MinValue) / (MaxValue - MinValue) * MeterHeight;//实际高度
           rect = GetTemplateChild("rect") as Rectangle;

           if (rect != null)
           {
               DoubleAnimation ani = new DoubleAnimation();
               ani.From = rect.ActualHeight;
               ani.To = h;
               if ((ani.From - ani.To).HasValue)
                   ani.Duration = TimeSpan.FromMilliseconds(Math.Abs((ani.From - ani.To).Value) * 10);
               else
                   ani.Duration = TimeSpan.FromMilliseconds(200);
               //rect.Height = h;
               rect.BeginAnimation(Rectangle.HeightProperty, ani);
           }
       }
       #endregion
   }
}
