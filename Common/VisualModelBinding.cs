using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace xControl.Simple.Common
{

    public class VisualModelBinding : Binding
    {
        public new object Source
        {
            get => base.Source;
            set
            {
                if (value is ViewModelBase vm)
                {
                    var avator = ViewModelAvator.CreateViewModelAvator(vm);
                    var props = avator.GetType().GetProperties();
                    if (avator != null)
                    {
                        base.Source = avator;
                    }
                }
            }
        }
    }
}
