using SampleProject.Common.Interfaces;
using SampleProject.Common.Data;

namespace SampleProject.DataAccess.DataProvider
{
    public class EmptyDataProvider : IDataProvider
    {
        public JsonData GetRateCollection()
        {
            return new JsonData();
        }
    }
}