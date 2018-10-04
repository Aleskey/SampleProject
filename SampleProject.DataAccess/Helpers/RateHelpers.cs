using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess.Helpers
{
    public class RateHelpers
    {
        public static IEnumerable<Rate> GetRatesForDayOfWeeks(
            IEnumerable<DayOfWeek> dayOfWeeks,
            TimeSpan fromTime,
            TimeSpan toTime,
            decimal price)
        {
            return dayOfWeeks.Select(dw => new Rate
            {
                DayOfWeek = dw,
                ToTime = toTime,
                FromTime = fromTime,
                Price = price
            });
        }
    }
}