using System.Collections.Generic;
using SampleProject.Data;
using SampleProject.DataAccess.Entities;

namespace SampleProject.Core.Factories.DataProviders
{
    public interface IDataProvider
    {
        RateCollection GetRateCollection();
    }
}