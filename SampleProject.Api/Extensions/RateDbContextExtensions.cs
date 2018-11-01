using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SampleProject.Core.Factories;
using SampleProject.DataAccess;
using SampleProject.DataAccess.Extensions;

namespace SampleProject.Api.Extensions
{
    public static class RateDbContextExtensions
    {
        public static void EnsureSeedDataForContext(this RateDbContext context, DataProviderFactory dataProviderFactory)
        {
            if (EnumerableExtensions.Any(context.Rates))
            {
                return;
            }

            var provider = dataProviderFactory.GetProvider();
            var rates = provider.GetRateCollection().GetRateEntities().ToList();

            if (!rates.Any())
            {
                return;
            }

            context.Rates.AddRange(rates);
            context.SaveChanges();
        }
    }
}
