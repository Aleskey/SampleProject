using System;
using System.Collections.Generic;
using System.Globalization;

namespace SampleProject.Common.Data
{
    public class Rate
    {
        public string Days { get; set; }

        public decimal Price { get; set; }

        public string Times { get; set; }

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

        public IEnumerable<DayOfWeek> ParseDays()
        {
            var items = (Days ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

            var result = new List<DayOfWeek>();

            foreach (var item in items)
            {
                if (!DayOfWeekMapping.TryGetValue(item.Trim().ToLowerInvariant(), out DayOfWeek dayOfWeek))
                {
                    throw new Exception("Invalid input, please ensure that you have set correct day of weeks e.g. mon,tues,wed");
                }

                result.Add(dayOfWeek);
            }

            return result;
        }

        public TimeSpanRange ParseTimes()
        {
            var items = (Times ?? string.Empty).Split('-', StringSplitOptions.RemoveEmptyEntries);

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
