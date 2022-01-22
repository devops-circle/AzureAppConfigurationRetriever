using System.Collections;
using System.Management.Automation;
using AzureAppConfigurationRetriever.Core.Implementations;
using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.PS.Commands
{
    [Cmdlet("Connect", "AzureAppConfiguration")]
    [OutputType(typeof(Hashtable))]
    public class ConnectAzureAppConfiguration : BaseCmdlet
    {
        public ConnectAzureAppConfiguration()
        {
        }

        public ConnectAzureAppConfiguration(CmdletDependencies cmdletDependencies): base(cmdletDependencies)
        {
        }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string EndPointUrl { get; set; }

        [Parameter(Mandatory = false)]
        public ConnectionType ConnectionType { get; set; }

        protected override void EndProcessing()
        {
            Core.ConnectionType azureConnectionType = (Core.ConnectionType)ConnectionType;

            IAzureAppConfigurationCredentialsConfig credentialsConfig =
                new AzureAppConfigurationCredentialsConfig(EndPointUrl, azureConnectionType);

            var sessionStateWrapper = GetSessionStateWrapper();
            
            sessionStateWrapper.SetVariable(this, new PSVariable("credentialConfig", credentialsConfig, ScopedItemOptions.Private));
        }
    }
}
