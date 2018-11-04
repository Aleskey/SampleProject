using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Common.Interfaces;
using SampleProject.Core.Interfaces;
using SampleProject.DataAccess.Entities;

namespace SampleProject.Core.Services
{
    public class RateCalculationService : IRateCalculationService
    {
        private IRepository<Rate> rateRepository;

        public RateCalculationService(IRepository<Rate> rateRepository)
        {
            this.rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        }

        public async Task<Rate> Calculate(DateTimeOffset fromDateTime, DateTimeOffset toDateTime)
        {
            var fromZulu = fromDateTime.ToUniversalTime();
            var toZulu = toDateTime.ToUniversalTime();

            var query =
                from rate in rateRepository.GetAll
                where rate.DayOfWeek == fromZulu.DayOfWeek
                      && fromZulu.TimeOfDay >= rate.FromTime
                      && rate.ToTime >= toZulu.TimeOfDay
                select rate;

            var result = await query.FirstOrDefaultAsync();

            return result;
        }
    }
}