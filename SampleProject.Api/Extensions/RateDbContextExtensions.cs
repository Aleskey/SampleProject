using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SampleProject.DataAccess;
using SampleProject.DataAccess.Helpers;

namespace SampleProject.Api.Extensions
{
    public static class RateDbContextExtensions
    {
        public static void EnsureSeedDataForContext(this RateDbContext context)
        {
            if (EnumerableExtensions.Any(context.Rates))
            {
                return;
            }

            var rates = RateHelpers.GetRatesForDayOfWeeks(
                    Enumerable.Range((int)DayOfWeek.Monday, (int)DayOfWeek.Friday).Cast<DayOfWeek>(),
                    new TimeSpan(6, 0, 0),
                    new TimeSpan(18, 0, 0),
                    1500)
                .Concat(RateHelpers.GetRatesForDayOfWeeks(
                    new[] { DayOfWeek.Saturday, DayOfWeek.Sunday },
                    new TimeSpan(6, 0, 0),
                    new TimeSpan(20, 0, 0),
                    2000)).ToList();

            context.Rates.AddRange(rates);
            context.SaveChanges();
        }
    }
}