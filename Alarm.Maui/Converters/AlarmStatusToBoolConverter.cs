using Alarm.Core.Enums;
using System.Globalization;

namespace Alarm.Maui.Converters
{
    public class AlarmStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is AlarmStatus status && status == AlarmStatus.On;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? AlarmStatus.On : AlarmStatus.Off;
        }
    }
}
