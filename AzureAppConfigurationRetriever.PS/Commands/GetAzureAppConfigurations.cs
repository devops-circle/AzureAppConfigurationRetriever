﻿using System.Management.Automation;
using System.Collections;

namespace AzureAppConfigurationRetriever.PS
{
    [Cmdlet(VerbsCommon.Get, "AzureAppConfigurations")]
    [OutputType(typeof(Hashtable))]
    public class GetAzureAppConfigurations : PSCmdlet
    {
  
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string EndPoint { get; set; }

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Label { get; set; }
       
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [PSDefaultValue(Value = false)]
        public bool MergeWithEmptyLabel { get; set; }

        protected override void BeginProcessing()
        {
        }

        protected override void ProcessRecord()
        {
            Hashtable result = AzureAppConfigurationRetriever.GetConfigurations(EndPoint, Label, MergeWithEmptyLabel);

            WriteObject(result);
        }

        protected override void EndProcessing()
        {
        }
    }
}
