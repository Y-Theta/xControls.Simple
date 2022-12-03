using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using System.Windows;
using System.Reflection.Metadata;
using static xControl.Simple.Utils.Interop;
using System.Runtime.InteropServices;

namespace xControl.Simple.Utils
{
    public class WindowOptionEx
    {
        #region   HideIcon
        public static readonly DependencyProperty HideIconProperty = DependencyProperty.RegisterAttached(
         "HideIcon", typeof(bool), typeof(BlurEffect), new PropertyMetadata(false, OnHideIconChanged));

        private static void OnHideIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                SetHideIcon(window, (bool)e.NewValue);
            }
        }

        public static void SetHideIcon(Window element, bool value)
        {
            //由于某些原因在我的vs和net版本下 如果此方法的名称为  SetBlurProperty 则可以起作用，
            //但在xaml中对应的属性名称就会变成"BlurProperty"，否则在Xaml下设置Blur属性值时此方法不会
            //触发，因此只能在OnBlurChanged中更改此方法
            try
            {
                IntPtr hwnd = IntPtr.Zero;
                if (element.IsInitialized)
                {
                    hwnd = GetVisualHandle(element);
                    SwitchIconStatus(hwnd, value);
                }
                else
                {
                    element.SourceInitialized += (s, e) => {
                        hwnd = GetVisualHandle(s as Window);
                        SwitchIconStatus(hwnd, value);
                    };
                }
            }
            catch
            {
            }
        }

        private static void SwitchIconStatus(IntPtr hwnd, bool hide)
        {
            int extendedStyle = Interop.GetWindowLong(hwnd, GWL_EXSTYLE);
            if (hide)
            {
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);
            }
            else
            {
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | ~WS_EX_DLGMODALFRAME);
            }
            // Update the window's non-client area to reflect the changes
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE |
                  SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }
        #endregion

        #region   Blur
        public static readonly DependencyProperty BlurProperty = DependencyProperty.RegisterAttached(
           "Blur", typeof(AccentState), typeof(BlurEffect), new PropertyMetadata(AccentState.ACCENT_DISABLED, OnBlurChanged));

        public static void SetBlur(FrameworkElement element, AccentState value)
        {
            //由于某些原因在我的vs和net版本下 如果此方法的名称为  SetBlurProperty 则可以起作用，
            //但在xaml中对应的属性名称就会变成"BlurProperty"，否则在Xaml下设置Blur属性值时此方法不会
            //触发，因此只能在OnBlurChanged中更改此方法
            try
            {
                IntPtr hwnd = IntPtr.Zero;
                if (element.IsInitialized)
                {
                    hwnd = GetVisualHandle(element);
                    EnableBlur(hwnd, value);
                }
                else
                {
                    element.Loaded += (s, e) =>
                    {
                        hwnd = GetVisualHandle(element);
                        EnableBlur(hwnd, value);
                    };
                }
            }
            catch
            {
            }
        }

        private static void OnBlurChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            SetBlur(element, (AccentState)e.NewValue);
        }

        private static void EnableBlur(IntPtr hwnd, AccentState state)
        {

            var accent = new AccentPolicy
            {
                AccentState = state
            };

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(hwnd, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
        #endregion
    }
}
