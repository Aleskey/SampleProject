using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SampleProject.DataAccess;
using SampleProject.DataAccess.Helpers;

namespace SampleProject.Api.Tests.Extensions
{
    public static class RateDbContextExtensions
    {
        public static void EnsureSeedTestDataForContext(this RateDbContext context)
        {
            if (EnumerableExtensions.Any(context.Rates))
            {
                return;
            }

            var rates =
                RateHelpers.GetRatesForDayOfWeeks(
                        new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday },
                        new TimeSpan(9, 0, 0),
                        new TimeSpan(21, 0, 0),
                        1500).Concat(
                        RateHelpers.GetRatesForDayOfWeeks(
                            new[] { DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                            new TimeSpan(9, 0, 0),
                            new TimeSpan(21, 0, 0),
                            2000)).Concat(
                        RateHelpers.GetRatesForDayOfWeeks(
                            new[] { DayOfWeek.Wednesday },
                            new TimeSpan(6, 0, 0),
                            new TimeSpan(18, 0, 0),
                            1750)).Concat(
                        RateHelpers.GetRatesForDayOfWeeks(
                            new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Saturday },
                            new TimeSpan(1, 0, 0),
                            new TimeSpan(5, 0, 0),
                            1000))
                    .Concat(
                        RateHelpers.GetRatesForDayOfWeeks(
                            new[] { DayOfWeek.Sunday, DayOfWeek.Tuesday },
                            new TimeSpan(1, 0, 0),
                            new TimeSpan(7, 0, 0),
                            925)).ToList();

            context.Rates.AddRange(rates);
            context.SaveChanges();
        }
    }
}