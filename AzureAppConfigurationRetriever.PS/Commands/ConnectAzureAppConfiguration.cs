using System.Collections;
using System.Management.Automation;
using System.Security;
using AzureAppConfigurationRetriever.Core;
using AzureAppConfigurationRetriever.Core.Implementations;
using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.PS.Commands
{
    [Cmdlet("Connect", "AzureAppConfiguration")]
    [CmdletBinding(DefaultParameterSetName = "Default")]
    [OutputType(typeof(Hashtable))]
    public class ConnectAzureAppConfiguration : BaseCmdlet
    {
        public ConnectAzureAppConfiguration()
        {
        }

        public ConnectAzureAppConfiguration(CmdletDependencies cmdletDependencies): base(cmdletDependencies)
        {
        }

        [ValidateNotNullOrEmpty]
        [Parameter(ParameterSetName = "DefaultConnection", Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = "ConnectionString", Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = "AzureCliConnection", Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = "PowerShellConnection", Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = "ManagedIdentityConnection", Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = "VisualStudioConnection", Mandatory = true, Position = 0)]
        [Parameter(ParameterSetName = "VisualStudioCodeConnection", Mandatory = true, Position = 0)]
        public string EndPointUrl { get; set; }

        [Parameter(ParameterSetName = "DefaultConnection", Mandatory = false)]
        public SwitchParameter UseDefaultConnection { get; set; }

        [Parameter(ParameterSetName = "AzureCliConnection", Mandatory = true)]
        public SwitchParameter UseAzureCliConnection { get; set; }

        [Parameter(ParameterSetName = "ManagedIdentityConnection", Mandatory = true)]
        public SwitchParameter UseManagedIdentityConnection { get; set; }

        [Parameter(ParameterSetName = "PowerShellConnection", Mandatory = true)]
        public SwitchParameter UseAzurePowerShellConnection { get; set; }

        [Parameter(ParameterSetName = "VisualStudioConnection", Mandatory = true)]
        public SwitchParameter UseVisualStudioConnection { get; set; }

        [Parameter(ParameterSetName = "VisualStudioCodeConnection", Mandatory = true)]
        public SwitchParameter UseVisualStudioCodeConnection { get; set; }

        [Parameter(ParameterSetName = "ConnectionString", Mandatory = true)]
        public SecureString ConnectionString { get; set; }

        protected override void EndProcessing()
        {
            ConnectionType connectionType;

            string parameterSetName = String.IsNullOrEmpty(this.ParameterSetName) ? this.ParamterSetUsed : this.ParameterSetName;

            switch (parameterSetName)
            {
                case "DefaultConnection":
                    connectionType = ConnectionType.Default;
                    break;
                case "AzureCliConnection":
                    connectionType = ConnectionType.AzureCli;
                    break;
                case "PowerShellConnection":
                    connectionType = ConnectionType.AzurePowerShell;
                    break;
                case "ManagedIdentityConnection":
                    connectionType = ConnectionType.ManagedIdentity;
                    break;
                case "VisualStudioConnection":
                    connectionType = ConnectionType.VisualStudio;
                    break;
                case "VisualStudioCodeConnection":
                    connectionType = ConnectionType.VisualStudioCode;
                    break;
                case "ConnectionString":
                    connectionType = ConnectionType.ConnectionString;
                    break;
                default:
                    throw new CmdletInvocationException("No parameter set found!");
            }

            IAzureAppConfigurationCredentialsConfig credentialsConfig =
                new AzureAppConfigurationCredentialsConfig(EndPointUrl, connectionType, ConnectionString);

            var sessionStateWrapper = GetSessionStateWrapper();
            
            sessionStateWrapper.SetVariable(this, new PSVariable("credentialConfig", credentialsConfig, ScopedItemOptions.Private));
        }
    }
}
