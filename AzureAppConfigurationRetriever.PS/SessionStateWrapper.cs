using System.Management.Automation;

namespace AzureAppConfigurationRetriever.PS
{
    public class SessionStateWrapper: ISessionStateWrapper
    {
        public void SetVariable(PSCmdlet cmdlet, PSVariable psVariable)
        {
            cmdlet.SessionState.PSVariable.Set(psVariable);
        }

        public void RemoveVariable(PSCmdlet cmdlet, string name)
        {
            cmdlet.SessionState.PSVariable.Remove(name);
        }

        public PSVariable GetVariable(PSCmdlet cmdlet, string name)
        {
            return cmdlet.SessionState.PSVariable.Get(name);
        }
    }
}
