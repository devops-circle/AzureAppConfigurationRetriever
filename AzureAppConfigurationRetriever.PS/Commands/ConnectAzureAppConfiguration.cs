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
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string EndPointUrl { get; set; }

        [Parameter(Mandatory = false)]
        public ConnectionType ConnectionType { get; set; }

        protected override void BeginProcessing()
        {
        }

        protected override void ProcessRecord()
        {
        }

        protected override void EndProcessing()
        {
            Core.ConnectionType azureConnectionType = (Core.ConnectionType)ConnectionType;

            IAzureAppConfigurationCredentialsConfig credentialsConfig =
                new AzureAppConfigurationCredentialsConfig(EndPointUrl, azureConnectionType);

            SessionState.PSVariable.Set(new PSVariable("credentialConfig", credentialsConfig, ScopedItemOptions.Private));
        }
    }
}
