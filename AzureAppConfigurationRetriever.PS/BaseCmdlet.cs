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
                throw new CmdletInvocationException("Run Connect-AzureAppConfiguration first.");
            }

            if (SessionState.PSVariable.Get("credentialConfig").Value is IAzureAppConfigurationCredentialsConfig azureAppConfigurationCredentialsConfig)
            {
                IAzureAppConfigurationCredentials azureAppConfigurationCredentials = new AzureAppConfigurationCredentials(azureAppConfigurationCredentialsConfig);

                return azureAppConfigurationCredentials;
            }
            else
            {
                throw new CmdletInvocationException("No credentials found.");
            }
        }
    }
}
