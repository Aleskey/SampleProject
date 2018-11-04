using System.Linq;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Entities;
using SampleProject.DataAccess.Extensions;

namespace SampleProject.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static void EnsureSeedData(
            this IUnitOfWork unitOfWork,
            IDataProviderFactory dataProviderFactory)
        {
            var rateRepository = unitOfWork.GetRepository<Rate>();

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
