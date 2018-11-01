using System;
using Microsoft.Extensions.Configuration;
using SampleProject.Core.Factories.DataProviders;

namespace SampleProject.Core.Factories
{
    public class DataProviderFactory
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