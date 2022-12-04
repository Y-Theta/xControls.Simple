using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace xControl.Simple.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public class ObservablePropAttribute : Attribute { }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
