using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleProject.Common.Interfaces;
using SampleProject.DataAccess.Entities;
using SampleProject.DataAccess.Extensions;

namespace SampleProject.Api.Initializers
{
    public static class ApplicationDbInitializer
    {
        public static async Task Seed(IUnitOfWork unitOfWork, IDataProviderFactory dataProviderFactory)
        {
            var rateRepository = unitOfWork.GetRepository<Rate>();

            if (rateRepository.GetAll.Any())
            {
                return;
            }

            await PopulateRateData(rateRepository, dataProviderFactory);
            unitOfWork.SaveChanges();
        }

        private static async Task PopulateRateData(IRepository<Rate> rateRepository, IDataProviderFactory dataProviderFactory)
        {
            var provider = dataProviderFactory.GetProvider();
            var jsonData = await provider.GetJsonData();
            var rates = jsonData.GetRateEntities();

            rateRepository.AddRange(rates);
        }
    }
}
