using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace xControl.Simple.Common
{
    public abstract class ValueConverterBase<T> :IValueConverter
        where T : IValueConverter, new() 
        
    {
        private static Lazy<T> _instance = new Lazy<T>(() => new T());
        public static T Singleton { get => _instance.Value; }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        protected ValueConverterBase() { }
    }
}
