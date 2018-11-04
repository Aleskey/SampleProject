using System.Collections.Generic;
using System.Linq;
using SampleProject.Common.Data;

namespace SampleProject.DataAccess.Extensions
{
    public static class RateCollectionExtensions
    {
        public static IEnumerable<Entities.Rate> GetRateEntities(this RateCollection collection)
        {
            var rates =
                (from rate in collection.Rates ?? Enumerable.Empty<Rate>()
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