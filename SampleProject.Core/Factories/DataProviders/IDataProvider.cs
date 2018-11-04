using SampleProject.Data;

namespace SampleProject.Core.Factories.DataProviders
{
    public interface IDataProvider
    {
        RateCollection GetRateCollection();
    }
}