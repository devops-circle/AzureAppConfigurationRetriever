using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Azure.Data.AppConfiguration;
using AzureAppConfigurationRetriever.Core.Interfaces;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace AzureAppConfigurationRetriever.Core.Implementations
{
    public class AzureAppConfigurationRetriever : IAzureAppConfigurationRetriever
    {
        private readonly IAzureAppConfigurationCredentials _azureAppConfigurationCredentials;

        public AzureAppConfigurationRetriever(IAzureAppConfigurationCredentials azureAppConfigurationCredentials)
        {
            _azureAppConfigurationCredentials = azureAppConfigurationCredentials;
        }

        public Hashtable GetConfigurationsByLabel(string label = "", bool mergeWithEmptyLabel = true)
        {
            var client = _azureAppConfigurationCredentials.GetClient();
            
            Dictionary<string, string> retValue;

            if (String.IsNullOrEmpty(label))
            {
                label = LabelFilter.Null;
            }

            var labelSetting = client.GetConfigurationSettings(new SettingSelector() { LabelFilter = label }).ToDictionary(x => x.Key, x => x.Value);

            //Parse the setting without label
            if (mergeWithEmptyLabel && !String.IsNullOrEmpty(label))
            {
                var emptyLabelSettings = client.GetConfigurationSettings(new SettingSelector() { LabelFilter = LabelFilter.Null }).ToDictionary(x => x.Key, x => x.Value);

                List<Dictionary<string, string>> labelSettingsDictionaryList = new List<Dictionary<string, string>>();
                labelSettingsDictionaryList.Add(emptyLabelSettings);
                labelSettingsDictionaryList.Add(labelSetting);

                retValue = MergeDictionaries(labelSettingsDictionaryList);
            }
            else
            {
                retValue = labelSetting;
            }

            return new Hashtable(retValue);
        }
        private static Dictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> dictionaries) where TKey : notnull
        {
            if (dictionaries is null)
            {
                throw new ArgumentNullException(nameof(dictionaries));
            }

            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            var dictionariesList = dictionaries.ToList();
            foreach (Dictionary<TKey, TValue> unused in dictionariesList)
            {
                return dictionariesList.SelectMany(x => x)
                 .ToLookup(pair => pair.Key, pair => pair.Value)
                 .ToDictionary(g => g.Key, g => g.Last());
            }

            return result;
        }
    }
}