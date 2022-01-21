using System.Collections;
using System.Management.Automation;

namespace AzureAppConfigurationRetriever.PS.Commands
{
    [Cmdlet("Disconnect", "AzureAppConfiguration")]
    [OutputType(typeof(Hashtable))]
    public class DisconnectAzureAppConfiguration : BaseCmdlet
    {
        protected override void EndProcessing()
        {
            SessionState.PSVariable.Remove("credentialConfig");
        }
    }
}
