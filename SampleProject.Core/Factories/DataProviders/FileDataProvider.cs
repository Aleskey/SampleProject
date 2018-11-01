using System;
using System.IO;
using Newtonsoft.Json;
using SampleProject.Data;

namespace SampleProject.Core.Factories.DataProviders
{
    public class FileDataProvider : IDataProvider
    {
        private string filePath;

        public FileDataProvider(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public RateCollection GetRateCollection()
        {
            var content = this.ReadFileContent();
            var result = JsonConvert.DeserializeObject<RateCollection>(content);

            return result;
        }

        private string  ReadFileContent()
        {
            return File.ReadAllText(this.filePath);
        }
    }
}