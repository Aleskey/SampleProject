using System;
using System.IO;
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