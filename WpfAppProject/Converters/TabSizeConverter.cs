using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfAppProject.Converters
{
    internal class TabSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] tabs, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TabControl tabControl = tabs[0] as TabControl;
            double width = tabControl.ActualWidth / tabControl.Items.Count;

            return (width <= 1) ? 0 : (width - 1);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
