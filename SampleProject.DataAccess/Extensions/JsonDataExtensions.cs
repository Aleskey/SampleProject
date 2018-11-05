using System.Collections.Generic;
using System.Linq;
using SampleProject.Common.Data;

namespace SampleProject.DataAccess.Extensions
{
    public static class JsonDataExtensions
    {
        public static IEnumerable<Entities.Rate> GetRateEntities(this JsonData data)
        {
            var rates =
                (from rate in data.Rates ?? Enumerable.Empty<Rate>()
                let timesRange = rate.ParseTimes()
                from dw in rate.ParseDays()
                select new Entities.Rate
                {
                    DayOfWeek = dw,
                    FromTime = timesRange.FromTime,
                    ToTime = timesRange.ToTime,
                    Price = rate.Price
                }).ToList();

            return rates;
        }
    }
}