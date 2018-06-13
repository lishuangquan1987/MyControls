using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//微信等待控件 作者：李双全
namespace WetChatWait
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WetChatWait"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WetChatWait;assembly=WetChatWait"
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
     [TemplatePart(Name = "rootGrid", Type = typeof(Grid))]
    public class WetChatWaitControl : UserControl
    {
        #region 私有变量
        private Grid rootGrid;
        private List<Ellipse> lstElipse = new List<Ellipse>();

        private const string circleSet = "圆球相关属性设置";

        #endregion

        #region 构造函数和初始化加载
        public WetChatWaitControl()
        {            
            this.Loaded += WaitControl_Loaded;
        }
        static WetChatWaitControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WetChatWaitControl), new FrameworkPropertyMetadata(typeof(WetChatWaitControl)));
        }
        void WaitControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnApplyTemplate();
        }
        #endregion

        #region 依赖属性
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(WetChatWaitControl),
            new PropertyMetadata((double)5, new PropertyChangedCallback(WetChatWaitControl.OnRadiusPropertyChanged)));



        public static readonly DependencyProperty CircleCountProperty =
            DependencyProperty.Register("CircleCount", typeof(int), typeof(WetChatWaitControl),
            new PropertyMetadata(4, new PropertyChangedCallback(WetChatWaitControl.OnCircleCountPropertyChanged)));

        public static readonly DependencyProperty IsActiveProperty =
           DependencyProperty.Register("IsActive", typeof(bool), typeof(WetChatWaitControl),
           new PropertyMetadata(false, new PropertyChangedCallback(WetChatWaitControl.OnIsActivePropertyChanged)));

        public static readonly DependencyProperty AnimationDistanceProperty =
          DependencyProperty.Register("AnimationDistance", typeof(double), typeof(WetChatWaitControl),
          new PropertyMetadata((double)40, null));

        public static readonly DependencyProperty AnimationTimeProperty =
          DependencyProperty.Register("AnimationTime", typeof(int), typeof(WetChatWaitControl),
          new PropertyMetadata(200, null));

        public static readonly DependencyProperty AnimationDelayTimeProperty =
          DependencyProperty.Register("AnimationDelayTime", typeof(int), typeof(WetChatWaitControl),
          new PropertyMetadata(500, null));

        public static readonly DependencyProperty ColorProperty =
         DependencyProperty.Register("Color", typeof(Brush), typeof(WetChatWaitControl),
         new PropertyMetadata(new SolidColorBrush(Colors.Red), WetChatWaitControl.OnColorPropertyChanged));
        #endregion

        #region 方法
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WetChatWaitControl w = d as WetChatWaitControl;
            if (w.lstElipse != null && w.lstElipse.Count != 0)
            {
                w.lstElipse.ForEach(x => x.Fill = w.Color);
            }
        }
        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WetChatWaitControl w = d as WetChatWaitControl;
            w.rootGrid = w.GetTemplateChild("rootGrid") as Grid;
            if (w.IsActive && w.rootGrid != null)
            {
                w.StartAnimation(null, null);
            }
        }

        private static void OnCircleCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WetChatWaitControl w = d as WetChatWaitControl;
            w.rootGrid = w.GetTemplateChild("rootGrid") as Grid;
            if (w.rootGrid == null)
                return;
            OnPropertyChanged(w);

        }
        private static void OnPropertyChanged(WetChatWaitControl w)
        {
            bool isActive = w.IsActive;
            w.IsActive = false;//先停止动画
            int animationTime = w.AnimationTime;
            int animationDelayTime = w.AnimationDelayTime;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                if (isActive)
                {
                    Thread.Sleep((animationTime + animationDelayTime) * 2); //等待之前的动画结束
                }
                w.Dispatcher.Invoke((Action)(() =>
                {
                    w.OnApplyTemplate();
                    w.IsActive = isActive;//启动动画
                }));
            });
        }
        private static void OnRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WetChatWaitControl w = d as WetChatWaitControl;
            w.rootGrid = w.GetTemplateChild("rootGrid") as Grid;
            if (w.rootGrid == null)
                return;
            OnPropertyChanged(w);
        }
        private void StartAnimation(object sender, EventArgs e)
        {
            if (rootGrid == null)
                return;
            if (!IsActive)
                return;
            if (lstElipse == null || lstElipse.Count == 0)
                return;
            Ellipse el = lstElipse[0];

            ThicknessAnimation ani = new ThicknessAnimation();
            ani.From = el.Margin;
            ani.To = new Thickness(el.Margin.Left - AnimationDistance, el.Margin.Top, 0, 0);
            ani.AutoReverse = true;
            ani.Duration = TimeSpan.FromMilliseconds(AnimationTime);
            ani.BeginTime = TimeSpan.FromMilliseconds(AnimationDelayTime);
            ani.Completed += AnimationCompleted;

            el.BeginAnimation(Ellipse.MarginProperty, ani);
        }

        private void AnimationCompleted(object sender, EventArgs e)
        {
            if (rootGrid == null)
                return;
            if (!IsActive)
                return;
            if (lstElipse == null || lstElipse.Count == 0)
                return;
            Ellipse el = lstElipse[lstElipse.Count - 1];

            ThicknessAnimation ani = new ThicknessAnimation();
            ani.From = el.Margin;
            ani.To = new Thickness(el.Margin.Left + AnimationDistance, el.Margin.Top, 0, 0);
            ani.AutoReverse = true;
            ani.Duration = TimeSpan.FromMilliseconds(AnimationTime);
            ani.BeginTime = TimeSpan.FromMilliseconds(AnimationDelayTime);
            ani.Completed += StartAnimation;
            el.BeginAnimation(Ellipse.MarginProperty, ani);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            rootGrid = GetTemplateChild("rootGrid") as Grid;

            if (this.rootGrid == null)
                return;
            this.lstElipse.Clear();
            this.rootGrid.Children.Clear();
            //求每个圆相对于rootGrid的坐标，思路：将这些圆组成一个虚拟长方形，然后将长方形放在rootGrid的正中央
            double height = this.rootGrid.ActualHeight;
            double width = this.rootGrid.ActualWidth;
            //求虚拟长方形的宽和高
            double virtualHeight = this.Radius * 2;
            double virtualWidth = this.Radius * 2 * this.CircleCount;
            //将虚拟长方形置中，并算出左边距和上边距
            double leftDistance = (width - virtualWidth) / (double)2;
            double topDistance = (height - virtualHeight) / (double)2;
            //计算每个圆形的margin坐标
            for (int i = 0; i < this.CircleCount; i++)
            {
                Ellipse el = new Ellipse();
                el.HorizontalAlignment = HorizontalAlignment.Left;
                el.VerticalAlignment = VerticalAlignment.Top;
                el.Height = el.Width = this.Radius * 2;//半径为长和宽的0.5倍
                el.Margin = new Thickness(leftDistance + i * this.Radius * 2, topDistance, 0, 0);
                el.Fill = this.Color;
                this.rootGrid.Children.Add(el);
                this.lstElipse.Add(el);
            }
            //执行动画
            this.StartAnimation(null, null);
        }
        #endregion

        #region 公有属性
        /// <summary>
        /// 圆球的半径
        /// </summary>
        [Description("设置圆球的半径"), Category(WetChatWaitControl.circleSet)]
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        [Description("设置圆球的个数"), Category(WetChatWaitControl.circleSet)]
        public int CircleCount
        {
            get { return (int)GetValue(CircleCountProperty); }
            set { SetValue(CircleCountProperty, value); }
        }
        /// <summary>
        /// 是否在运行
        /// </summary>
        [Description("设置圆球是否摆动"),Category(WetChatWaitControl.circleSet)]
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
         [Description("设置圆球摆动的距离"), Category(WetChatWaitControl.circleSet)]
        public double AnimationDistance
        {
            get { return (double)GetValue(AnimationDistanceProperty); }
            set { SetValue(AnimationDistanceProperty, value); }
        }
        //动画时间，以毫秒为单位
        [Description("设置圆球摆动来回的时间，单位为毫秒"), Category(WetChatWaitControl.circleSet)]
        public int AnimationTime
        {
            get { return (int)GetValue(AnimationTimeProperty); }
            set { SetValue(AnimationTimeProperty, value); }
        }
        //经过多长时间，动画开始，动画延迟时间
         [Description("设置圆球动画交替的延迟时间，单位为毫秒"), Category(WetChatWaitControl.circleSet)]
        public int AnimationDelayTime
        {
            get { return (int)GetValue(AnimationDelayTimeProperty); }
            set { SetValue(AnimationDelayTimeProperty, value); }
        }
        //圆球的颜色
        [Description("设置圆球的颜色，单位为毫秒"), Category(WetChatWaitControl.circleSet)]
        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        #endregion
    }
}
