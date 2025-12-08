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

    public class WindowEx : Window
    {

        #region   Props
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr WindHandle { get; private set; }

        /// <summary>
        /// 边框弧度
        /// </summary>
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(WindowEx), 
                new PropertyMetadata(0.0));

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
