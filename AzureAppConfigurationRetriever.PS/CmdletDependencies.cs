using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.PS
{
    public class CmdletDependencies
    {
        public IAzureAppConfigurationCredentials AzureAppConfigurationCredentials { get; set; }
        public ISessionStateWrapper SessionStateWrapper { get; set; }

        public string ParameterSetNameUsed { get; set; }
    }
}
