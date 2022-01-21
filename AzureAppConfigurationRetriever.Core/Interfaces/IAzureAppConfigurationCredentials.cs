using Azure.Data.AppConfiguration;

namespace AzureAppConfigurationRetriever.Core.Interfaces
{
    public interface IAzureAppConfigurationCredentials
    {
        public ConfigurationClient GetClient();
    }
}
