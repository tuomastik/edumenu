using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Edumenu.Models;

namespace Edumenu
{
    public class SelectedDayToForeground : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new SolidColorBrush(Colors.White);
            }
            if (((string)value).ToLower().Equals(Globals.SelectedDay.ToLower()))
            {
                return Application.Current.Resources["ThemeColor4"];
            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
