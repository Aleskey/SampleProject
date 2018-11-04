using SampleProject.Common.Interfaces;
using SampleProject.Common.Data;

namespace SampleProject.DataAccess.DataProvider
{
    public class EmptyDataProvider : IDataProvider
    {
        public RateCollection GetRateCollection()
        {
            return new RateCollection();
        }
    }
}