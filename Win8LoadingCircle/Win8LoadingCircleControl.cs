using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
/*
 * 此控件为从网上下载后更改，加入了很多依赖属性
 */
namespace Win8LoadingCircle
{
    
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfControlLibraryDemo"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfControlLibraryDemo;assembly=WpfControlLibraryDemo"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:Loading/>
    ///
    /// </summary>
    public class Win8LoadingCircleControl : Control
    {
        static Win8LoadingCircleControl()
        {
            #region 样式说明
            //此控件的内容和样式都是在文件Generic.xaml进行定义的，即可视为UI设计
            // 经验证，必须有,RelativeSource={RelativeSource TemplatedParent} 否则绑定失效
            // <SolidColorBrush x:Key = "ParticleColor" Color = "{Binding Path=FillColor,RelativeSource={RelativeSource TemplatedParent}}" />


            #endregion           
            //重载默认样式
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Win8LoadingCircleControl), new FrameworkPropertyMetadata(typeof(Win8LoadingCircleControl)));
                                
        }
        #region 依赖属性
        public static readonly DependencyProperty ParticleColorProperty = DependencyProperty.Register("ParticleColor",
                typeof(Brush), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(new SolidColorBrush(Colors.DarkBlue), null));

        public static readonly DependencyProperty ParticleBackgroundColorProperty = DependencyProperty.Register("ParticleBackgroundColor",
                typeof(Brush), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(new SolidColorBrush(Colors.Transparent), null));

        public static readonly DependencyProperty ParticleOpacityProperty = DependencyProperty.Register("ParticleOpacity",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)1, null));

        public static readonly DependencyProperty ParticleRadiusProperty = DependencyProperty.Register("ParticleRadius",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)3, null));

        public static readonly DependencyProperty StartingPointXProperty = DependencyProperty.Register("StartingPointX",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)0, null));

        public static readonly DependencyProperty StartingPointYProperty = DependencyProperty.Register("StartingPointY",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)-10, null));

        public static readonly DependencyProperty RotationPointXProperty = DependencyProperty.Register("RotationPointX",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)0.5, null));

        public static readonly DependencyProperty RotationPointYProperty = DependencyProperty.Register("RotationPointY",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)0.5, null));



        public static readonly DependencyProperty StoryBoardBeginTimeP0Property = DependencyProperty.Register("StoryBoardBeginTimeP0",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:00.000"), null));
        public static readonly DependencyProperty StoryBoardBeginTimeP1Property = DependencyProperty.Register("StoryBoardBeginTimeP1",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:00.100"), null));
        public static readonly DependencyProperty StoryBoardBeginTimeP2Property = DependencyProperty.Register("StoryBoardBeginTimeP2",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:00.200"), null));
        public static readonly DependencyProperty StoryBoardBeginTimeP3Property = DependencyProperty.Register("StoryBoardBeginTimeP3",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:00.300"), null));
        public static readonly DependencyProperty StoryBoardBeginTimeP4Property = DependencyProperty.Register("StoryBoardBeginTimeP4",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:00.400"), null));
        public static readonly DependencyProperty StoryBoardDurationProperty = DependencyProperty.Register("StoryBoardDuration",
                typeof(Duration), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(new Duration(TimeSpan.Parse("00:00:01.800")), null));

        public static readonly DependencyProperty ParticleOriginAngleP0Property = DependencyProperty.Register("ParticleOriginAngleP0",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)0, null));
        public static readonly DependencyProperty ParticleOriginAngleP1Property = DependencyProperty.Register("ParticleOriginAngleP1",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)-10, null));
        public static readonly DependencyProperty ParticleOriginAngleP2Property = DependencyProperty.Register("ParticleOriginAngleP2",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)-20, null));
        public static readonly DependencyProperty ParticleOriginAngleP3Property = DependencyProperty.Register("ParticleOriginAngleP3",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)-30, null));
        public static readonly DependencyProperty ParticleOriginAngleP4Property = DependencyProperty.Register("ParticleOriginAngleP4",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)-40, null));

        public static readonly DependencyProperty ParticleBeginAngle1Property = DependencyProperty.Register("ParticleBeginAngle1",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)0, null));
        public static readonly DependencyProperty ParticleEndAngle1Property = DependencyProperty.Register("ParticleEndAngle1",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)90, null));
        public static readonly DependencyProperty ParticleBeginTime1Property = DependencyProperty.Register("ParticleBeginTime1",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:00.00"), null));
        public static readonly DependencyProperty ParticleDuration1Property = DependencyProperty.Register("ParticleDuration1",
                typeof(Duration), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(new Duration(TimeSpan.Parse("00:00:00.750")), null));



        public static readonly DependencyProperty ParticleBeginAngle2Property = DependencyProperty.Register("ParticleBeginAngle2",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)90, null));
        public static readonly DependencyProperty ParticleEndAngle2Property = DependencyProperty.Register("ParticleEndAngle2",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)270, null));
        public static readonly DependencyProperty ParticleBeginTime2Property = DependencyProperty.Register("ParticleBeginTime2",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:00.751"), null));
        public static readonly DependencyProperty ParticleDuration2Property = DependencyProperty.Register("ParticleDuration2",
                typeof(Duration), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(new Duration(TimeSpan.Parse("00:00:00.300")), null));



        public static readonly DependencyProperty ParticleBeginAngle3Property = DependencyProperty.Register("ParticleBeginAngle3",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)270, null));
        public static readonly DependencyProperty ParticleEndAngle3Property = DependencyProperty.Register("ParticleEndAngle3",
                typeof(double), typeof(Win8LoadingCircleControl), new UIPropertyMetadata((double)360, null));
        public static readonly DependencyProperty ParticleBeginTime3Property = DependencyProperty.Register("ParticleBeginTime3",
                typeof(TimeSpan), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(TimeSpan.Parse("00:00:01.052"), null));
        public static readonly DependencyProperty ParticleDuration3Property = DependencyProperty.Register("ParticleDuration3",
                typeof(Duration), typeof(Win8LoadingCircleControl), new UIPropertyMetadata(new Duration(TimeSpan.Parse("00:00:00.750")), null));
        #endregion
        
        #region 属性
        
        [Description("圆颗粒的颜色"), Category("圆颗粒配置")]
        public Brush ParticleColor { get { return (Brush)GetValue(ParticleColorProperty); } set { SetValue(ParticleColorProperty, value); } }
        [Description("控件的背景色")]
        public Brush ParticleBackgroundColor { get { return (Brush)GetValue(ParticleBackgroundColorProperty); } set { SetValue(ParticleBackgroundColorProperty, value); } }
        [Description("圆颗粒的透明度"),Category("圆颗粒配置")]
        public double ParticleOpacity { get { return (double)GetValue(ParticleOpacityProperty); } set { SetValue(ParticleOpacityProperty, value); } }
        [Description("圆颗粒的半径"),Category("圆颗粒配置")]
        public double ParticleRadius { get { return (double)GetValue(ParticleRadiusProperty); } set { SetValue(ParticleRadiusProperty, value); } }
        [Category("圆颗粒的围绕半径")]
        public double StartingPointX { get { return (double)GetValue(StartingPointXProperty); } set { SetValue(StartingPointXProperty, value); } }
        [Category("圆颗粒的围绕半径")]
        public double StartingPointY { get { return (double)GetValue(StartingPointYProperty); } set { SetValue(StartingPointYProperty, value); } }
        [Category("圆颗粒的围绕半径")]
        public double RotationPointX { get { return (double)GetValue(RotationPointXProperty); } set { SetValue(RotationPointXProperty, value); } }
        [Category("圆颗粒的围绕半径")]
        public double RotationPointY { get { return (double)GetValue(RotationPointYProperty); } set { SetValue(RotationPointYProperty, value); } }


        public TimeSpan StoryBoardBeginTimeP0 { get { return (TimeSpan)GetValue(StoryBoardBeginTimeP0Property); } set { SetValue(StoryBoardBeginTimeP0Property, value); } }
        public TimeSpan StoryBoardBeginTimeP1 { get { return (TimeSpan)GetValue(StoryBoardBeginTimeP1Property); } set { SetValue(StoryBoardBeginTimeP1Property, value); } }
        public TimeSpan StoryBoardBeginTimeP2 { get { return (TimeSpan)GetValue(StoryBoardBeginTimeP2Property); } set { SetValue(StoryBoardBeginTimeP2Property, value); } }
        public TimeSpan StoryBoardBeginTimeP3 { get { return (TimeSpan)GetValue(StoryBoardBeginTimeP3Property); } set { SetValue(StoryBoardBeginTimeP3Property, value); } }
        public TimeSpan StoryBoardBeginTimeP4 { get { return (TimeSpan)GetValue(StoryBoardBeginTimeP4Property); } set { SetValue(StoryBoardBeginTimeP4Property, value); } }
        public Duration StoryBoardDuration { get { return (TimeSpan)GetValue(StoryBoardDurationProperty); } set { SetValue(StoryBoardDurationProperty, value); } }


        public double ParticleOriginAngleP0 { get { return (double)GetValue(ParticleOriginAngleP0Property); } set { SetValue(ParticleOriginAngleP0Property, value); } }
        public double ParticleOriginAngleP1 { get { return (double)GetValue(ParticleOriginAngleP1Property); } set { SetValue(ParticleOriginAngleP1Property, value); } }
        public double ParticleOriginAngleP2 { get { return (double)GetValue(ParticleOriginAngleP2Property); } set { SetValue(ParticleOriginAngleP2Property, value); } }
        public double ParticleOriginAngleP3 { get { return (double)GetValue(ParticleOriginAngleP3Property); } set { SetValue(ParticleOriginAngleP3Property, value); } }
        public double ParticleOriginAngleP4 { get { return (double)GetValue(ParticleOriginAngleP4Property); } set { SetValue(ParticleOriginAngleP4Property, value); } }


        public double ParticleBeginAngle1 { get { return (double)GetValue(ParticleBeginAngle1Property); } set { SetValue(ParticleBeginAngle1Property, value); } }
        public double ParticleEndAngle1 { get { return (double)GetValue(ParticleEndAngle1Property); } set { SetValue(ParticleEndAngle1Property, value); } }
        public TimeSpan ParticleBeginTime1 { get { return (TimeSpan)GetValue(ParticleBeginTime1Property); } set { SetValue(ParticleBeginTime1Property, value); } }
        public Duration ParticleDuration1 { get { return (Duration)GetValue(ParticleDuration1Property); } set { SetValue(ParticleDuration1Property, value); } }


        public double ParticleBeginAngle2 { get { return (double)GetValue(ParticleBeginAngle2Property); } set { SetValue(ParticleBeginAngle2Property, value); } }
        public double ParticleEndAngle2 { get { return (double)GetValue(ParticleEndAngle2Property); } set { SetValue(ParticleEndAngle2Property, value); } }
        public TimeSpan ParticleBeginTime2 { get { return (TimeSpan)GetValue(ParticleBeginTime2Property); } set { SetValue(ParticleBeginTime2Property, value); } }
        public Duration ParticleDuration2 { get { return (Duration)GetValue(ParticleDuration2Property); } set { SetValue(ParticleDuration2Property, value); } }


        public double ParticleBeginAngle3 { get { return (double)GetValue(ParticleBeginAngle3Property); } set { SetValue(ParticleBeginAngle3Property, value); } }
        public double ParticleEndAngle3 { get { return (double)GetValue(ParticleEndAngle3Property); } set { SetValue(ParticleEndAngle3Property, value); } }
        public TimeSpan ParticleBeginTime3 { get { return (TimeSpan)GetValue(ParticleBeginTime3Property); } set { SetValue(ParticleBeginTime3Property, value); } }
        public Duration ParticleDuration3 { get { return (Duration)GetValue(ParticleDuration3Property); } set { SetValue(ParticleDuration3Property, value); } }



        #endregion
    }
}
