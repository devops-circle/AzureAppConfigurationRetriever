using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.PS
{
    public class CmdletDependencies
    {
        public IAzureAppConfigurationCredentials AzureAppConfigurationCredentials { get; set; }
    }
}
