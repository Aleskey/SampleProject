using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Common.Interfaces;
using SampleProject.Core.Interfaces;
using SampleProject.Core.Models;
using SampleProject.DataAccess.Entities;

namespace SampleProject.Core.Services
{
    public class RateFindService : IRateService
    {
        private readonly IUnitOfWork unitOfWork;

        public RateFindService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Rate> FindRateAsync(RateRequest rateRequest)
        {
            var fromZulu = rateRequest.FromDate.ToUniversalTime();
            var toZulu = rateRequest.ToDate.ToUniversalTime();

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