using System.Security;
using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.Core.Implementations
{
    public class AzureAppConfigurationCredentialsConfig: IAzureAppConfigurationCredentialsConfig
    {
        public AzureAppConfigurationCredentialsConfig(string endPointUrl, ConnectionType connectionType, SecureString connectionString)
        {
            EndPointUrl = endPointUrl;
            ConnectionType = connectionType;
            ConnectionString = connectionString;
        }

        public string EndPointUrl { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public SecureString ConnectionString { get; set; }
    }
}
