using System;
using System.Threading.Tasks;
using SampleProject.DataAccess.Entities;

namespace SampleProject.Core.Interfaces
{
    public interface IRateCalculationAction
    {
        Task<Rate> Calculate(DateTimeOffset fromDateTime, DateTimeOffset toDateTime);
    }
}