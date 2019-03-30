using System;
using System.Globalization;
using System.Linq;

namespace SmartHelmet.SignalrHub.Helpers
{
    public static class NmeaHelpers
    {
        public static string[] Parse(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            /*
            int checksum = -1;
            var idx = message.IndexOf('*');
            if (idx >= 0)
            {
                checksum = Convert.ToInt32(message.Substring(idx + 1), 16);
                message = message.Substring(0, message.IndexOf('*'));
            }
            if (checksum > -1)
            {
                int checksumTest = 0;
                for (int i = 1; i < message.Length; i++)
                {
                    checksumTest ^= Convert.ToByte(message[i]);
                }
                if (checksum != checksumTest)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid nmea message: Checksum failure. Got {0:X2}, Expected {1:X2}", checksum, checksumTest));
                }
            }
            */

            string[] parts = message.Split(new char[] { ',' });
            string[] messageParts = parts.Skip(1).ToArray();

            return messageParts;
        }

        internal static double StringToLatitude(string value, string ns)
        {
            if (value == null || value.Length < 3)
            {
                return double.NaN;
            }
            double latitude = int.Parse(value.Substring(0, 2), CultureInfo.InvariantCulture) + double.Parse(value.Substring(2), CultureInfo.InvariantCulture) / 60;
            if (ns == "S")
            {
                latitude *= -1;
            }
            return latitude;
        }

        internal static double StringToLongitude(string value, string ew)
        {
            if (value == null || value.Length < 4)
            {
                return double.NaN;
            }
            double longitude = int.Parse(value.Substring(0, 3), CultureInfo.InvariantCulture) + double.Parse(value.Substring(3), CultureInfo.InvariantCulture) / 60;
            if (ew == "W")
            {
                longitude *= -1;
            }
            return longitude;
        }
    }
}
