using System;
using System.Threading.Tasks;
using SampleProject.DataAccess.Entities;

namespace SampleProject.Core.Interfaces
{
    public interface IRateCalculationService
    {
        Task<Rate> Calculate(DateTimeOffset fromDateTime, DateTimeOffset toDateTime);
    }
}