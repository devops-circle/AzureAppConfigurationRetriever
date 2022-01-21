using System.Collections;
using System.Management.Automation;
using AzureAppConfigurationRetriever.Core.Interfaces;

namespace AzureAppConfigurationRetriever.PS.Commands
{
    [Cmdlet(VerbsCommon.Get, "AzureAppConfigurationsByLabel")]
    [OutputType(typeof(Hashtable))]
    public class GetAzureAppConfigurationsByLabel : BaseCmdlet
    {
        public GetAzureAppConfigurationsByLabel()
        {
        }

        public GetAzureAppConfigurationsByLabel(CmdletDependencies cmdletDependencies): base(cmdletDependencies)
        {
        }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string EndPoint { get; set; }

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Label { get; set; }
       
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [PSDefaultValue(Value = false)]
        public SwitchParameter MergeWithEmptyLabel { get; set; }

        protected override void BeginProcessing()
        {
        }
        
        protected override void ProcessRecord()
        {
            var azureAppConfigurationCredentials = base.GetAzureAppConfigurationCredentials();

            IAzureAppConfigurationRetriever retriever =
                new Core.Implementations.AzureAppConfigurationRetriever(azureAppConfigurationCredentials);

            Hashtable result = retriever.GetConfigurationsByLabel(Label, MergeWithEmptyLabel.IsPresent);
            
             WriteObject(result);
        }

        protected override void EndProcessing()
        {
        }
    }
}
