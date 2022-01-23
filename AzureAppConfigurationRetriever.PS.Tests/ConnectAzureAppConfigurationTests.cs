using Xunit;
using AzureAppConfigurationRetriever.Core.Interfaces;
using AzureAppConfigurationRetriever.PS.Commands;
using NSubstitute;

namespace AzureAppConfigurationRetriever.PS.Tests
{
    public class ConnectAzureAppConfigurationTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void ConnectHasSavedSessionVariable()
        {
            var azureAppConfigurationCredentials = Substitute.For<IAzureAppConfigurationCredentials>();
            var sessionStateWrapper = Substitute.For<ISessionStateWrapper>();

            CmdletDependencies deps = new CmdletDependencies()
            {
                AzureAppConfigurationCredentials = azureAppConfigurationCredentials,
                SessionStateWrapper = sessionStateWrapper,
                ParameterSetNameUsed = "DefaultConnection"
            };

            var psEmulator = new PowershellEmulator();
            var cmdlet = new ConnectAzureAppConfiguration(deps)
            {
                EndPointUrl = "https://marcoconfigtest.azconfig.io",
                UseDefaultConnection = true
            };
            cmdlet.CommandRuntime = psEmulator;

            cmdlet.ProcessInternal();
            var results = psEmulator.OutputObjects;

            sessionStateWrapper.ReceivedWithAnyArgs().SetVariable(default, default);
        }
    }
}