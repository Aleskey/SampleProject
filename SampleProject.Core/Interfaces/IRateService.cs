using System;
using System.Threading.Tasks;
using SampleProject.Core.Models;
using SampleProject.DataAccess.Entities;

namespace SampleProject.Core.Interfaces
{
    public interface IRateService
    {
        Task<Rate> FindRateAsync(RateRequest rateRequest);
    }
}