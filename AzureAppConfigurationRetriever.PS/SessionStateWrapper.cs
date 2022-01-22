using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace AzureAppConfigurationRetriever.PS
{
    public class SessionStateWrapper: ISessionStateWrapper
    {
        public void SetVariable(PSCmdlet cmdlet, PSVariable psVariable)
        {
            cmdlet.SessionState.PSVariable.Set(psVariable);
        }
    }
}
