using Azure.Data.AppConfiguration;
using Azure.Identity;
using AzureAppConfigurationRetriever.Core.Interfaces;
using System;

namespace AzureAppConfigurationRetriever.Core.Implementations
{
    public class AzureAppConfigurationCredentials: IAzureAppConfigurationCredentials
    {
        private readonly IAzureAppConfigurationCredentialsConfig _appConfigurationCredentialsConfig;

        public AzureAppConfigurationCredentials(IAzureAppConfigurationCredentialsConfig azureAppConfigurationCredentialsConfig)
        {
            _appConfigurationCredentialsConfig = azureAppConfigurationCredentialsConfig;
        }

        public ConfigurationClient GetClient()
        {
            ConfigurationClient client;

            switch (_appConfigurationCredentialsConfig.ConnectionType)
            {
                case ConnectionType.AzurePowerShell:
                    var azurePowerShellCred = new AzurePowerShellCredential();
                    client = new ConfigurationClient(new Uri(_appConfigurationCredentialsConfig.EndPointUrl), azurePowerShellCred);
                    break;
                case ConnectionType.AzureCli:
                    var azureCliCred = new AzureCliCredential();
                    client = new ConfigurationClient(new Uri(_appConfigurationCredentialsConfig.EndPointUrl), azureCliCred);
                    break;
                case ConnectionType.ManagedIdentity:
                    var managedIdentityCredential = new ManagedIdentityCredential();
                    client = new ConfigurationClient(new Uri(_appConfigurationCredentialsConfig.EndPointUrl), managedIdentityCredential);
                    break;
                case ConnectionType.VisualStudio:
                    var visualStudioCredential = new VisualStudioCredential();
                    client = new ConfigurationClient(new Uri(_appConfigurationCredentialsConfig.EndPointUrl), visualStudioCredential);
                    break;
                case ConnectionType.VisualStudioCode:
                    var visualStudioCodeCredential = new VisualStudioCodeCredential();
                    client = new ConfigurationClient(new Uri(_appConfigurationCredentialsConfig.EndPointUrl), visualStudioCodeCredential);
                    break;
                case ConnectionType.ConnectionString:
                    client = new ConfigurationClient(_appConfigurationCredentialsConfig.ConnectionString);
                    break;
                case ConnectionType.Environment:
                    string connection = Environment.GetEnvironmentVariable("AZURE_APP_CONFIGURATION_CONN") ?? throw new InvalidOperationException("Environment variable AZURE_APP_CONFIGURATION_CONN not found or empty");
                    client = new ConfigurationClient(connection);
                    break;
                case ConnectionType.Default:
                default:
                    var defaultCred = new DefaultAzureCredential();
                    client = new ConfigurationClient(new Uri(_appConfigurationCredentialsConfig.EndPointUrl), defaultCred);
                    break;
            }
            return client;
        }
    }
}
