using Xunit;

namespace AzureAppConfigurationRetriever.PS.IntegrationTests
{
    public class ConnectAzureAppConfigurationIntegrationTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void Execute_ConnectCmdLet()
        {
            using var powerShell = PSHostHelper.GetPowerShellHost();
            powerShell.AddCommand("Connect-AzureAppConfiguration");
            powerShell.AddParameter("EndPointUrl", "https://marcoconfigtest.azconfig.io");

            var results = powerShell.Invoke();

            Assert.Empty(results);
        }
    }
}