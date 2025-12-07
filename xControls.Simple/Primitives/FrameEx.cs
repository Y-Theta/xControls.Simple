using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace xControl.Simple.Primitives
{
    public class FrameEx : Frame
    {
        public FrameEx()
        {
        }

        static FrameEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FrameEx), new FrameworkPropertyMetadata(typeof(FrameEx)));
        }
    }
}
