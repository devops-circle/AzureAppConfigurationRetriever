using System.Collections.Generic;
using System.Collections.ObjectModel;
using Azure;
using Azure.Data.AppConfiguration;
using AzureAppConfigurationRetriever.Core.Interfaces;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using NSubstitute;
using Xunit;

namespace AzureAppConfigurationRetriever.Core.Tests
{
    public class AzureAppConfigurationRetrieverTests
    {
        [Fact]
        public void RetrieveWithEmptyLabelAndNoSelector()
        {
            List<ConfigurationSetting> configurationSettingsList_1 = new List<ConfigurationSetting>();
            ConfigurationSetting sett_1_1 = new ConfigurationSetting("name", "marco");
            ConfigurationSetting sett_1_2 = new ConfigurationSetting("surname", "mansi");

            configurationSettingsList_1.Add(sett_1_1);
            configurationSettingsList_1.Add(sett_1_2);

            IReadOnlyList<ConfigurationSetting> configReadOnlyListList_1 = new ReadOnlyCollection<ConfigurationSetting>(configurationSettingsList_1);

            Page<ConfigurationSetting> configPage_1 = Page<ConfigurationSetting>.FromValues(configReadOnlyListList_1, "", Substitute.For<Response>());

            List<ConfigurationSetting> configurationSettingsList_2 = new List<ConfigurationSetting>();
            ConfigurationSetting sett_2_1 = new ConfigurationSetting("hobby", "cloud");
            ConfigurationSetting sett_2_2 = new ConfigurationSetting("specialism", "azure");

            configurationSettingsList_2.Add(sett_2_1);
            configurationSettingsList_2.Add(sett_2_2);

            IReadOnlyList<ConfigurationSetting> configReadOnlyListList_2 = new ReadOnlyCollection<ConfigurationSetting>(configurationSettingsList_2);

            Page<ConfigurationSetting> configPage_2 = Page<ConfigurationSetting>.FromValues(configReadOnlyListList_2, "", Substitute.For<Response>());

            Pageable<ConfigurationSetting> configPageable = Pageable<ConfigurationSetting>.FromPages(new[] { configPage_1, configPage_2 });

            var azureAppConfigurationCredentials = Substitute.For<IAzureAppConfigurationCredentials>();

            azureAppConfigurationCredentials.GetClient().GetConfigurationSettings(Arg.Is<SettingSelector>(sel => sel.LabelFilter == LabelFilter.Null )).Returns(configPageable);

            Implementations.AzureAppConfigurationRetriever retriever =
                new Implementations.AzureAppConfigurationRetriever(azureAppConfigurationCredentials);

            //No selector
            var result = retriever.GetConfigurationsByLabel();

            Assert.Equal(4, result.Count);
            Assert.True(result.ContainsKey("name"));
            Assert.True(result["name"]?.ToString() == "marco");
            Assert.True(result.ContainsKey("surname"));
            Assert.True(result["surname"]?.ToString() == "mansi");
        }
    }
}