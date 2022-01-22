using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace AzureAppConfigurationRetriever.PS.IntegrationTests
{
    public static class PSHostHelper
    {
        public static PowerShell GetPowerShellHost()
        {
            string modulePath = IntegrationTestHelper.GetModulePath();

            var iss = InitialSessionState.CreateDefault();

            iss.ImportPSModule(new[]
            {
                modulePath, "-Force"
            });

            var ps = PowerShell.Create(iss);
            return ps;
        }
    }
}
