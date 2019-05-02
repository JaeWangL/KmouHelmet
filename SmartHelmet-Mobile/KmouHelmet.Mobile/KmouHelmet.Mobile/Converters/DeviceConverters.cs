using System;
using System.Globalization;
using Xamarin.Forms;

namespace KmouHelmet.Mobile.Converters
{
    public class DeviceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                throw new InvalidOperationException("The target must be a string");
            }

            var deviceId = (string)value;
            if (deviceId.Equals("8"))
            {
                return Color.Red;
            }
            if (deviceId.Equals("9"))
            {
                return Color.Blue;
            }

            return Color.FromHex("#252525");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
