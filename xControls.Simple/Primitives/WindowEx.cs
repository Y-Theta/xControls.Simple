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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using xControl.Simple.Utils;

namespace xControl.Simple.Primitives
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:xControl.Simple.Primitives"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:xControl.Simple.Primitives;assembly=xControl.Simple.Primitives"
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
    ///     <MyNamespace:WindowEx/>
    ///
    /// </summary>
    public class WindowEx : Window
    {

        #region   Props
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr WindHandle { get; private set; }

        /// <summary>
        /// 次要按钮样式
        /// </summary>
        public Style CmdButtonStyle
        {
            get { return (Style)GetValue(CmdButtonStyleProperty); }
            set { SetValue(CmdButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty CmdButtonStyleProperty =
            DependencyProperty.Register(nameof(CmdButtonStyle), typeof(Style), typeof(WindowEx), 
                new PropertyMetadata(null));

        /// <summary>
        /// 关闭按钮样式
        /// </summary>
        public Style PrimCmdButtonStyle
        {
            get { return (Style)GetValue(PrimCmdButtonStyleProperty); }
            set { SetValue(PrimCmdButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty PrimCmdButtonStyleProperty =
            DependencyProperty.Register(nameof(PrimCmdButtonStyle), typeof(Style), typeof(WindowEx),
                new PropertyMetadata(null));

        /// <summary>
        /// 标题区域 Style
        /// </summary>
        public Style TitleStyle
        {
            get { return (Style)GetValue(TitleStyleProperty); }
            set { SetValue(TitleStyleProperty, value); }
        }
        public static readonly DependencyProperty TitleStyleProperty =
            DependencyProperty.Register(nameof(TitleStyle), typeof(Style), typeof(WindowEx), 
                new PropertyMetadata(null));

        /// <summary>
        /// 标题区域高度
        /// </summary>
        public double TitleAreaHeight
        {
            get { return (double)GetValue(TitleAreaHeightProperty); }
            set { SetValue(TitleAreaHeightProperty, value); }
        }
        public static readonly DependencyProperty TitleAreaHeightProperty =
            DependencyProperty.Register(nameof(TitleAreaHeight), typeof(double), typeof(WindowEx), 
                new PropertyMetadata(0.0));

        /// <summary>
        /// 状态栏高度
        /// </summary>
        public double StatusAreaHeight
        {
            get { return (double)GetValue(StatusAreaHeightProperty); }
            set { SetValue(StatusAreaHeightProperty, value); }
        }
        public static readonly DependencyProperty StatusAreaHeightProperty =
            DependencyProperty.Register(nameof(StatusAreaHeight), typeof(double), typeof(WindowEx), 
                new PropertyMetadata(0.0));

        /// <summary>
        /// 标题栏区域
        /// </summary>
        public object TitleArea
        {
            get { return (object)GetValue(TitleAreaProperty); }
            set { SetValue(TitleAreaProperty, value); }
        }
        public static readonly DependencyProperty TitleAreaProperty =
            DependencyProperty.Register(nameof(TitleArea), typeof(object), typeof(WindowEx),
                new PropertyMetadata(null));

        /// <summary>
        /// 状态栏区域
        /// </summary>
        public object StatusBarArea
        {
            get { return (object)GetValue(StatusBarAreaProperty); }
            set { SetValue(StatusBarAreaProperty, value); }
        }
        public static readonly DependencyProperty StatusBarAreaProperty =
            DependencyProperty.Register(nameof(StatusBarArea), typeof(object), typeof(WindowEx),
                new PropertyMetadata(null));
        #endregion


        protected override void OnSourceInitialized(EventArgs e)
        {
            WindHandle = Interop.GetVisualHandle(this);
            HwndSource source = HwndSource.FromHwnd(WindHandle);
            source.AddHook(WndProc);
            base.OnSourceInitialized(e);
        }

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            return IntPtr.Zero;
        }

        static WindowEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowEx), new FrameworkPropertyMetadata(typeof(WindowEx)));
        }
    }
}
