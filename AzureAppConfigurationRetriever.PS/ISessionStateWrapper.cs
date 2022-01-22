using System.Management.Automation;

namespace AzureAppConfigurationRetriever.PS
{
    public interface ISessionStateWrapper
    {
        public void SetVariable(PSCmdlet cmdlet, PSVariable psVariable);
    }
}
