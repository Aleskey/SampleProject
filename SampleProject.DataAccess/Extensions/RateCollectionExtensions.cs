using System.Collections.Generic;
using System.Linq;
using SampleProject.Common.Data;
using SampleProject.Common.Parsers;

namespace SampleProject.DataAccess.Extensions
{
    public static class RateCollectionExtensions
    {
        public static IEnumerable<Entities.Rate> GetRateEntities(this RateCollection collection)
        {
            var rates =
                (from rate in collection.Rates
                let timesRange = TimesParser.Parse(rate.Times)
                from dw in DayOfWeekParser.Parse(rate.Days)
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