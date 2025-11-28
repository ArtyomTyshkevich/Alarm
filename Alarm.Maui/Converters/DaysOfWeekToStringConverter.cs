using System.Globalization;

namespace Alarm.Maui.Converters
{
    public class DaysOfWeekToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<DayOfWeek> days && days.Count > 0)
            {
                return string.Join(", ", days.Select(d => d.ToString().Substring(0, 3)));
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new List<DayOfWeek>();
        }
    }
}
