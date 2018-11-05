using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SampleProject.Common.Interfaces;
using SampleProject.Common.Data;

namespace SampleProject.DataAccess.DataProvider
{
    public class FileDataProvider : IDataProvider
    {
        private readonly string filePath;

        public FileDataProvider(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public async Task<JsonData> GetJsonData()
        {
            var content = await File.ReadAllTextAsync(this.filePath);
            var result = JsonConvert.DeserializeObject<JsonData>(content);
            
            return result;
        }
    }
}