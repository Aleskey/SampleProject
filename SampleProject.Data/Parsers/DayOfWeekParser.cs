using System;
using System.Collections.Generic;

namespace SampleProject.Data.Parsers
{
    public static class DayOfWeekParser
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

        public static IEnumerable<DayOfWeek> Parse(string data)
        {
            var items = (data ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

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
    }
}