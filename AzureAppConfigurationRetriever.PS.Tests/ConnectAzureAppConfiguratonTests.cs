using System.Management.Automation;
using Xunit;
using System.Management.Automation.Runspaces;
using AzureAppConfigurationRetriever.Core.Interfaces;
using AzureAppConfigurationRetriever.PS.Commands;
using NSubstitute;

namespace AzureAppConfigurationRetriever.PS.Tests
{
    public class ConnectAzureAppConfiguratonTests
    {
        [Fact]
        [Trait("Category","Integration")]
        public void Execute_ConnectCmdLet()
        {
            using var powerShell = PSHostHelper.GetPowerShellHost();
            powerShell.AddCommand("Connect-AzureAppConfiguration");
            powerShell.AddParameter("EndPointUrl", "https://marcoconfigtest.azconfig.io");

            dynamic results = powerShell.Invoke();
            //Assert.That(ResultOk(results));
        }
        public static class PSHostHelper
        {
            public static PowerShell GetPowerShellHost()
            {
                var iss = InitialSessionState.CreateDefault();
#if DEBUG
                iss.ImportPSModule(new[]
                {
                    @"D:\Sources\DevOpsCircle\AzureAppConfigurationRetriever\AzureAppConfigurationRetriever.PS.Tests\bin\Debug\net6.0\AzureAppConfigurationRetriever.PS.dll",
                    "-Force"
                });
#else
            iss.ImportPSModule(new[] { @"..\..\..\CoreView.Management.PowerShell\bin\Release\net48\CoreView.Management.PowerShell.dll", "-Force" });
#endif
                var ps = PowerShell.Create(iss);
                return ps;
            }
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void ConnectHasSavedSessionVariable()
        {
            var azureAppConfigurationCredentials = Substitute.For<IAzureAppConfigurationCredentials>();
            var sessionStateWrapper = Substitute.For<ISessionStateWrapper>();

            CmdletDependencies deps = new CmdletDependencies()
            {
                AzureAppConfigurationCredentials = azureAppConfigurationCredentials,
                SessionStateWrapper = sessionStateWrapper
            };

            var psEmulator = new PowershellEmulator();
            var cmdlet = new ConnectAzureAppConfiguration(deps)
            {
                EndPointUrl = "https://marcoconfigtest.azconfig.io"
            };
            cmdlet.CommandRuntime = psEmulator;

            cmdlet.ProcessInternal();
            var results = psEmulator.OutputObjects;

            sessionStateWrapper.ReceivedWithAnyArgs().SetVariable(default, default);
        }
    }
}