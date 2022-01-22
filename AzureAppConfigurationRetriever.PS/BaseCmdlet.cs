using AzureAppConfigurationRetriever.Core.Implementations;
using AzureAppConfigurationRetriever.Core.Interfaces;
using System.Management.Automation;

namespace AzureAppConfigurationRetriever.PS
{
    public class BaseCmdlet : PSCmdlet
    {
        internal IAzureAppConfigurationCredentials AzureAppConfigurationCredentials { get; set; }
        internal ISessionStateWrapper SessionStateWrapper { get; set; }

        public BaseCmdlet()
        {
        }

        public BaseCmdlet(CmdletDependencies cmdletDependencies)
        {
            AzureAppConfigurationCredentials = cmdletDependencies.AzureAppConfigurationCredentials;
            SessionStateWrapper = cmdletDependencies.SessionStateWrapper;
        }

        internal void ProcessInternal()
        {
            BeginProcessing();
            ProcessRecord();
            EndProcessing();
        }

        internal IAzureAppConfigurationCredentials GetAzureAppConfigurationCredentials()
        {
            if (AzureAppConfigurationCredentials != null)
            {
                return AzureAppConfigurationCredentials;
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

            throw new CmdletInvocationException("No credentials found.");
            
        }

        internal ISessionStateWrapper getSessionStateWrapper()
        {
            if (SessionStateWrapper != null)
            {
                return SessionStateWrapper;
            }

            SessionStateWrapper sessionStateWrapper = new SessionStateWrapper();

            return sessionStateWrapper;
        }

    }
}
