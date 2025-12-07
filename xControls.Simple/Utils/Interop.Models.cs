using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xControl.Simple.Utils
{
    public static partial class Interop
    {
        internal const int GWL_EXSTYLE = -20;
        internal const int WS_EX_DLGMODALFRAME = 0x0001;
        internal const int SWP_NOSIZE = 0x0001;
        internal const int SWP_NOMOVE = 0x0002;
        internal const int SWP_NOZORDER = 0x0004;
        internal const int SWP_FRAMECHANGED = 0x0020;
        internal const int WM_SETICON = 0x0080;

        internal enum WindowCompositionAttribute
        {
            // 省略其他未使用的字段
            WCA_ACCENT_POLICY = 19,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct _RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /// <summary>
        /// 控件特效
        /// </summary>
        public enum AccentState
        {
            /// <summary>
            /// 不启用
            /// </summary>
            ACCENT_DISABLED,
            /// <summary>
            /// 不明
            /// </summary>
            ACCENT_ENABLE_GRADIENT,
            ACCENT_ENABLE_TRANSPARENTGRADIENT,
            /// <summary>
            /// 毛玻璃
            /// </summary>
            ACCENT_ENABLE_BLURBEHIND,
            ACCENT_INVALID_STATE,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

    }
}
