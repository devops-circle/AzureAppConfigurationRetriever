using Azure.Data.AppConfiguration;

namespace AzureAppConfigurationRetriever.Core.Interfaces
{
    public interface IAzureAppConfigurationClientFactory
    {
        public ConfigurationClient GetClient();
    }
}
