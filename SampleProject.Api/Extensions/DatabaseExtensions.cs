using System.Linq;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Entities;
using SampleProject.DataAccess.Extensions;

namespace SampleProject.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static void EnsureSeedData(this IUnitOfWork unitOfWork, IDataProviderFactory dataProviderFactory)
        {
            var rateRepository = unitOfWork.GetRepository<Rate>();

            if (rateRepository.GetAll.Any())
            {
                return;
            }

            PopulateData(unitOfWork, dataProviderFactory);
            unitOfWork.SaveChanges();
        }

        private static void PopulateData(IUnitOfWork unitOfWork, IDataProviderFactory dataProviderFactory)
        {
            var rateRepository = unitOfWork.GetRepository<Rate>();

            var provider = dataProviderFactory.GetProvider();
            var rates = provider.GetRateCollection().GetRateEntities().ToList();

            rateRepository.AddRange(rates);
        }
    }
}
