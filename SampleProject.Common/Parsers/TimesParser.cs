using System;
using System.Globalization;
using SampleProject.Common.Data;

namespace SampleProject.Common.Parsers
{
    public class TimesParser
    {
        private const string TimeFormat = "hhmm";

        public static TimeSpanRange Parse(string data)
        {
            var items = (data ?? string.Empty).Split('-', StringSplitOptions.RemoveEmptyEntries);

            if (items.Length != 2)
            {
                throw new ArgumentException("Invalid data string, correct format must be following to 0100-1000");
            }

            var fromTime = TimeSpan.ParseExact(items[0], TimeFormat, CultureInfo.InvariantCulture);
            var toTime = TimeSpan.ParseExact(items[1], TimeFormat, CultureInfo.InvariantCulture);

            return new TimeSpanRange(fromTime, toTime);
        }
    }
}