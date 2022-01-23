using System.Collections;
using System.Management.Automation;
using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.PS.Commands
{
    [Cmdlet(VerbsCommon.Get, "AzureAppConfiguration")]
    [OutputType(typeof(string))]
    public class GetAzureAppConfiguration : BaseCmdlet
    {
        public GetAzureAppConfiguration()
        {
        }

        public GetAzureAppConfiguration(CmdletDependencies cmdletDependencies): base(cmdletDependencies)
        {
        }

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string ValueName { get; set; }

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Label { get; set; }

        protected override void ProcessRecord()
        {
            var azureAppConfigurationCredentials = base.GetAzureAppConfigurationCredentials();

            IAzureAppConfigurationRetriever retriever =
                new Core.Implementations.AzureAppConfigurationRetriever(azureAppConfigurationCredentials);

            var result = retriever.GetConfiguration(ValueName,Label);
            
             WriteObject(result);
        }
    }
}
