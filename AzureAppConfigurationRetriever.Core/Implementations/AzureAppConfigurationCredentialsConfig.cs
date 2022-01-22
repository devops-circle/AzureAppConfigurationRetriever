using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.Core.Implementations
{
    public class AzureAppConfigurationCredentialsConfig: IAzureAppConfigurationCredentialsConfig
    {
        public AzureAppConfigurationCredentialsConfig(string endPointUrl, ConnectionType connectionType)
        {
            EndPointUrl = endPointUrl;
            ConnectionType = connectionType;
        }

        public string EndPointUrl { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public string ConnectionString { get; set; }
    }
}
