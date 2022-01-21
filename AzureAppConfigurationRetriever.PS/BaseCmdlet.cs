using AzureAppConfigurationRetriever.Core.Implementations;
using AzureAppConfigurationRetriever.Core.Interfaces;
using System.Management.Automation;

namespace AzureAppConfigurationRetriever.PS
{
    public class BaseCmdlet : PSCmdlet
    {
        internal IAzureAppConfigurationCredentials _azureAppConfigurationCredentials { get; set; }

        public BaseCmdlet()
        {
        }

        public BaseCmdlet(CmdletDependencies cmdletDependencies)
        {
            _azureAppConfigurationCredentials = cmdletDependencies.AzureAppConfigurationCredentials;
        }

        internal IAzureAppConfigurationCredentials GetAzureAppConfigurationCredentials()
        {
            if (_azureAppConfigurationCredentials != null)
            {
                return _azureAppConfigurationCredentials;
            }

            if (SessionState.PSVariable.Get("credentialConfig") == null)
            {
                throw new Exception("Run Connect-AzureAppConfiguration first.");
            }

            var azureAppConfigurationCredentialsConfig = SessionState.PSVariable.Get("credentialConfig").Value as IAzureAppConfigurationCredentialsConfig;

            IAzureAppConfigurationCredentials creds = new AzureAppConfigurationCredentials(azureAppConfigurationCredentialsConfig);

            return creds;
        }
    }
}
