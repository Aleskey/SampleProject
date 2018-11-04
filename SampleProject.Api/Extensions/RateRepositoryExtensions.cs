using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess;
using SampleProject.DataAccess.DataProvider;
using SampleProject.DataAccess.Entities;
using SampleProject.DataAccess.Extensions;

namespace SampleProject.Api.Extensions
{
    public static class RateRepositoryExtensions
    {
        public static void EnsureSeedData(
            this IRepository<Rate> rateRepository,
            IDataProviderFactory dataProviderFactory,
            IUnitOfWork unitOfWork)
        {
            if (rateRepository.GetAll.Any())
            {
                return;
            }

            var provider = dataProviderFactory.GetProvider();
            var rates = provider.GetRateCollection().GetRateEntities().ToList();

            if (!rates.Any())
            {
                return;
            }

            rateRepository.AddRange(rates);
            unitOfWork.SaveChanges();
        }
    }
}
