using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AzureAppConfigurationRetriever.Core.Implementations;
using AzureAppConfigurationRetriever.Core.Interfaces;
using Xunit;

namespace AzureAppConfigurationRetriever.Core.Tests
{
    public class AzureAppConfigurationClientFactoryTests
    {
        [Theory]
        [InlineData(ConnectionType.AzurePowerShell)]
        [InlineData(ConnectionType.AzureCli)]
        [InlineData(ConnectionType.ConnectionString)]
        [InlineData(ConnectionType.VisualStudioCode)]
        [InlineData(ConnectionType.VisualStudio)]
        [InlineData(ConnectionType.ManagedIdentity)]
        [InlineData(ConnectionType.Default)]
        public void TestGetClient(ConnectionType conn)
        {
            string stringTosecure = "Endpoint=https://thisisatest.azconfig.io;Id=UF/d-l9-s0:hNR1xbVi/TZ5LFMWQ0Eu;Secret=18F458OQpyFGzxhSj08lAIriXF3UoUS7xNqxM/iObzg=";

            SecureString theSecureString = new NetworkCredential("", stringTosecure).SecurePassword;
            IAzureAppConfigurationCredentialsConfig config =
                new AzureAppConfigurationCredentialsConfig("https://test.com", conn, theSecureString);

            AzureAppConfigurationClientFactory creds = new AzureAppConfigurationClientFactory(config);

            var client = creds.GetClient();

            Assert.NotNull(client);


        }

    }
}
