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
            var configPageable = CreateConfigListWithoutLabel();

            var azureAppConfigurationCredentials = Substitute.For<IAzureAppConfigurationClientFactory>();

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

        [Fact]
        public void RetrieveWithLabel()
        {
            var configPageable = CreateConfigListWithLabel("test");
            var configPageable2 = CreateConfigListWithoutLabel();

            var azureAppConfigurationCredentials = Substitute.For<IAzureAppConfigurationClientFactory>();

            azureAppConfigurationCredentials.GetClient().GetConfigurationSettings(default).ReturnsForAnyArgs(configPageable, configPageable2);

            Implementations.AzureAppConfigurationRetriever retriever =
                new Implementations.AzureAppConfigurationRetriever(azureAppConfigurationCredentials);

            var result = retriever.GetConfigurationsByLabel("test", true);
            Assert.Equal(5, result.Count);
            Assert.True(result.ContainsKey("name"));
            Assert.True(result["name"]?.ToString() == "marco_withlabel");
            Assert.True(result.ContainsKey("surname"));
            Assert.True(result["surname"]?.ToString() == "mansi_withlabel");
            Assert.True(result.ContainsKey("favoritecolor"));
            Assert.True(result["favoritecolor"]?.ToString() == "red_withlabel");

        }

        private static Pageable<ConfigurationSetting> CreateConfigListWithoutLabel()
        {
            List<ConfigurationSetting> configurationSettingsList_1 = new List<ConfigurationSetting>();
            ConfigurationSetting sett_1_1 = new ConfigurationSetting("name", "marco");
            ConfigurationSetting sett_1_2 = new ConfigurationSetting("surname", "mansi");

            configurationSettingsList_1.Add(sett_1_1);
            configurationSettingsList_1.Add(sett_1_2);

            IReadOnlyList<ConfigurationSetting> configReadOnlyListList_1 =
                new ReadOnlyCollection<ConfigurationSetting>(configurationSettingsList_1);

            Page<ConfigurationSetting> configPage_1 =
                Page<ConfigurationSetting>.FromValues(configReadOnlyListList_1, "", Substitute.For<Response>());

            List<ConfigurationSetting> configurationSettingsList_2 = new List<ConfigurationSetting>();
            ConfigurationSetting sett_2_1 = new ConfigurationSetting("hobby", "cloud");
            ConfigurationSetting sett_2_2 = new ConfigurationSetting("specialism", "azure");

            configurationSettingsList_2.Add(sett_2_1);
            configurationSettingsList_2.Add(sett_2_2);

            IReadOnlyList<ConfigurationSetting> configReadOnlyListList_2 =
                new ReadOnlyCollection<ConfigurationSetting>(configurationSettingsList_2);

            Page<ConfigurationSetting> configPage_2 =
                Page<ConfigurationSetting>.FromValues(configReadOnlyListList_2, "", Substitute.For<Response>());

            Pageable<ConfigurationSetting> configPageable =
                Pageable<ConfigurationSetting>.FromPages(new[] {configPage_1, configPage_2});
            return configPageable;
        }

        private static Pageable<ConfigurationSetting> CreateConfigListWithLabel(string label)
        {
            List<ConfigurationSetting> configurationSettingsList_1 = new List<ConfigurationSetting>();
            ConfigurationSetting sett_1_1 = new ConfigurationSetting("name", "marco_withlabel", label);
            ConfigurationSetting sett_1_2 = new ConfigurationSetting("surname", "mansi_withlabel", label);

            configurationSettingsList_1.Add(sett_1_1);
            configurationSettingsList_1.Add(sett_1_2);

            IReadOnlyList<ConfigurationSetting> configReadOnlyListList_1 =
                new ReadOnlyCollection<ConfigurationSetting>(configurationSettingsList_1);

            Page<ConfigurationSetting> configPage_1 =
                Page<ConfigurationSetting>.FromValues(configReadOnlyListList_1, "", Substitute.For<Response>());

            List<ConfigurationSetting> configurationSettingsList_2 = new List<ConfigurationSetting>();
            ConfigurationSetting sett_2_1 = new ConfigurationSetting("hobby", "cloud_withlabel", label);
            ConfigurationSetting sett_2_2 = new ConfigurationSetting("favoritecolor", "red_withlabel", label);

            configurationSettingsList_2.Add(sett_2_1);
            configurationSettingsList_2.Add(sett_2_2);

            IReadOnlyList<ConfigurationSetting> configReadOnlyListList_2 =
                new ReadOnlyCollection<ConfigurationSetting>(configurationSettingsList_2);

            Page<ConfigurationSetting> configPage_2 =
                Page<ConfigurationSetting>.FromValues(configReadOnlyListList_2, "", Substitute.For<Response>());

            Pageable<ConfigurationSetting> configPageable =
                Pageable<ConfigurationSetting>.FromPages(new[] {configPage_1, configPage_2});
            return configPageable;
        }
    }
}