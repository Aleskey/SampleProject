using System;
using Microsoft.Extensions.Configuration;
using SampleProject.Common.Interfaces;

namespace SampleProject.DataAccess.DataProvider
{
    public class DataProviderFactory : IDataProviderFactory
    {
        private IConfiguration configuration;

        private const string ConfigurationFilePathKey = "DataFile";

        public DataProviderFactory(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IDataProvider GetProvider()
        {
            var filePath = this.configuration[ConfigurationFilePathKey];

            if (string.IsNullOrEmpty(filePath))
            {
                return new EmptyDataProvider();
            }

            return new FileDataProvider(filePath);
        }
    }
}