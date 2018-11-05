using System.Threading.Tasks;
using SampleProject.Common.Interfaces;
using SampleProject.Common.Data;

namespace SampleProject.DataAccess.DataProvider
{
    public class EmptyDataProvider : IDataProvider
    {
        public async Task<JsonData> GetJsonData()
        {
            return await Task.FromResult(new JsonData());
        }
    }
}