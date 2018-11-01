using System.Collections.Generic;
using System.Linq;
using SampleProject.Data;
using SampleProject.Data.Parsers;
using SampleProject.DataAccess.Entities;

namespace SampleProject.DataAccess.Extensions
{
    public static class RateCollectionExtensions
    {
        public static IEnumerable<Rate> GetRateEntities(this RateCollection collection)
        {
            var rates =
                (from rate in collection.Rates
                let timesRange = TimesParser.Parse(rate.Times)
                from dw in DayOfWeekParser.Parse(rate.Days)
                select new Rate
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