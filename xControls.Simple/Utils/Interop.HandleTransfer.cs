using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows;

namespace xControl.Simple.Utils
{
    public static partial class Interop
    {

		/// <summary>
		/// 获取控件的句柄
		/// </summary>
		/// <param name="visual"></param>
		/// <returns></returns>
		public static IntPtr GetVisualHandle(Visual visual)
		{
			IntPtr hwnd = IntPtr.Zero;
			var source = PresentationSource.FromVisual(visual);
			if (source != null)
				hwnd = ((HwndSource)source).Handle;
			return hwnd;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="visual"></param>
		/// <returns></returns>
		public static IntPtr GetVisualHandle(DependencyObject visual)
		{
			IntPtr hwnd = IntPtr.Zero;
			var source = PresentationSource.FromDependencyObject(visual);
			if (source != null)
				hwnd = ((HwndSource)source).Handle;
			return hwnd;
		}

	}
}
