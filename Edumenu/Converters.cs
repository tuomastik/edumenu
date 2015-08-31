using System;
using Windows.UI.Xaml.Data;

namespace Converters
{
    public class StringToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is string)
            {
                return ((string)value).ToUpper();
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // The value is being binded "OneWay" so ConvertBack will not get called
            throw new NotImplementedException();
        }
    }
}