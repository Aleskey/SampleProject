using SampleProject.Data;

namespace SampleProject.Core.Factories.DataProviders
{
    public class EmptyDataProvider : IDataProvider
    {
        public RateCollection GetRateCollection()
        {
            return new RateCollection();
        }
    }
}