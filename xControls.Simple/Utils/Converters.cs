using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using xControl.Simple.Common;

namespace xControl.Simple.Utils
{
    public class Converters
    {
        public class VisibilityConverter : ValueConverterBase<VisibilityConverter>
        {
            public enum ConvertMode
            {
                TheSame,
                OnTheContrary
            }

            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is null)
                    return Visibility.Collapsed;
                else if (value is string)
                    return string.IsNullOrWhiteSpace(value.ToString()) ? Visibility.Collapsed : Visibility.Visible;
                else if (value is bool)
                {
                    if (parameter is null)
                        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                    else if (parameter is ConvertMode para)
                    {
                        switch (para)
                        {
                            case ConvertMode.TheSame:
                                return (bool)value ? Visibility.Visible : Visibility.Hidden;
                            default: return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                        }
                    }
                    else
                        return Visibility.Visible;
                }
                else if (value is Enum)
                {
                    return value.ToString().Equals(parameter.ToString()) ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                    return Visibility.Visible;
            }

            public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new ArgumentNullException("This is a one way converter");
            }
        }

        public class ControlConverter : ValueConverterBase<ControlConverter>
        {

            public interface ICanEnable
            {
                bool CanEnable { get; }
            }

            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is ICanEnable ce)
                {
                    return ce.CanEnable;
                }

                if (value is string str && str == string.Empty)
                {
                    return false;
                }

                if (value is ICollection enu && enu.Count == 0)
                {
                    return false;
                }

                return true;
            }

            public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new ArgumentNullException("This is a one way converter");
            }
        }

        public class MultiConverter : IMultiValueConverter
        {
            public static readonly MultiConverter Singleton = new MultiConverter();

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values.All(v => v is bool))
                {
                    var all = values.All(v => (bool)v);
                    return all ? Visibility.Visible : Visibility.Collapsed;
                }

                return Visibility.Collapsed;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new ArgumentNullException("This is a one way converter");
            }
        }

    }
}
