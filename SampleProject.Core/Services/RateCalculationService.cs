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
        private readonly IUnitOfWork unitOfWork;

        public RateCalculationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Rate> Calculate(DateTimeOffset fromDateTime, DateTimeOffset toDateTime)
        {
            var fromZulu = fromDateTime.ToUniversalTime();
            var toZulu = toDateTime.ToUniversalTime();

            var rateRepository = unitOfWork.GetRepository<Rate>();

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