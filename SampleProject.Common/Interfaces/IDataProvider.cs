using System.Threading.Tasks;
using SampleProject.Common.Data;

namespace SampleProject.Common.Interfaces
{
    public interface IDataProvider
    {
        Task<JsonData> GetJsonData();
    }
}