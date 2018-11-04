using System;
using System.Collections.Generic;
using System.Globalization;
using SampleProject.Common.Data;

namespace SampleProject.Common.Extensions
{
    public static class RateExtensions
    {
        private static readonly IDictionary<string, DayOfWeek> DayOfWeekMapping = new Dictionary<string, DayOfWeek>
        {
            { "mon", DayOfWeek.Monday },
            { "tues", DayOfWeek.Tuesday },
            { "wed", DayOfWeek.Wednesday },
            { "thurs", DayOfWeek.Thursday },
            { "fri", DayOfWeek.Friday },
            { "sat", DayOfWeek.Saturday },
            { "sun", DayOfWeek.Sunday }
        };

        private const string TimeFormat = "hhmm";

        public static IEnumerable<DayOfWeek> ParseDays(this Rate rate)
        {
            if (rate == null)
            {
                throw new ArgumentNullException(nameof(rate));
            }

            var items = (rate.Days ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

            var result = new List<DayOfWeek>();
            DayOfWeek dayOfWeek;

            foreach (var item in items)
            {
                if (!DayOfWeekMapping.TryGetValue(item.Trim().ToLowerInvariant(), out dayOfWeek))
                {
                    throw new Exception("Invalid input, please ensure that you have set correct day of weeks e.g. mon,tues,wed");
                }

                result.Add(dayOfWeek);
            }

            return result;
        }

        public static TimeSpanRange ParseTimes(this Rate rate)
        {
            if (rate == null)
            {
                throw new ArgumentNullException(nameof(rate));
            }

            var items = (rate.Times ?? string.Empty).Split('-', StringSplitOptions.RemoveEmptyEntries);

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