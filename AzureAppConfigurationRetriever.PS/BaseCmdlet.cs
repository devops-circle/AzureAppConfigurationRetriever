using AzureAppConfigurationRetriever.Core.Implementations;
using AzureAppConfigurationRetriever.Core.Interfaces;
using System.Management.Automation;

namespace AzureAppConfigurationRetriever.PS
{
    public class BaseCmdlet : PSCmdlet
    {
        internal IAzureAppConfigurationClientFactory AzureAppConfigurationCredentials { get; set; }
        internal ISessionStateWrapper SessionStateWrapper { get; set; }
        public BaseCmdlet()
        {
        }

        public BaseCmdlet(CmdletDependencies cmdletDependencies)
        {
            AzureAppConfigurationCredentials = cmdletDependencies.AzureAppConfigurationCredentials;
            SessionStateWrapper = cmdletDependencies.SessionStateWrapper;
            ParamterSetUsed = cmdletDependencies.ParameterSetNameUsed;
        }

        internal string ParamterSetUsed { get; set; }

        internal void ProcessInternal()
        {
            BeginProcessing();
            ProcessRecord();
            EndProcessing();
        }

        internal IAzureAppConfigurationClientFactory GetAzureAppConfigurationCredentials()
        {
            if (AzureAppConfigurationCredentials != null)
            {
                return AzureAppConfigurationCredentials;
            }

            var sessionStateWrapper = GetSessionStateWrapper();

            if (sessionStateWrapper.GetVariable(this,"credentialConfig") == null)
            {
                throw new CmdletInvocationException("Run Connect-AzureAppConfiguration first.");
            }

            if (sessionStateWrapper.GetVariable(this, "credentialConfig").Value is IAzureAppConfigurationCredentialsConfig azureAppConfigurationCredentialsConfig)
            {
                IAzureAppConfigurationClientFactory azureAppConfigurationCredentials = new AzureAppConfigurationClientFactory(azureAppConfigurationCredentialsConfig);

                return azureAppConfigurationCredentials;
            }

            throw new CmdletInvocationException("No credentials found.");
            
        }

        internal ISessionStateWrapper GetSessionStateWrapper()
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
