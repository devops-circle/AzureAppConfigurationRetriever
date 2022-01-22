using System.Management.Automation;

namespace AzureAppConfigurationRetriever.PS
{
    public interface ISessionStateWrapper
    {
        public void SetVariable(PSCmdlet cmdlet, PSVariable psVariable);

        public void RemoveVariable(PSCmdlet cmdlet, string name);

        public PSVariable GetVariable(PSCmdlet cmdlet, string name);
    }
}
