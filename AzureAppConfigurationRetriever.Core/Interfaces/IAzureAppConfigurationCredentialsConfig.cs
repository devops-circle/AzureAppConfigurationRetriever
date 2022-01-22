using System.Security;

namespace AzureAppConfigurationRetriever.Core.Interfaces
{
    public interface IAzureAppConfigurationCredentialsConfig
    {
        public string EndPointUrl { get; set; }

        public ConnectionType ConnectionType { get; set; }

        public SecureString ConnectionString { get; set; }
    }
}
