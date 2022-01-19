using Azure.Data.AppConfiguration;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System.Collections;

namespace AzureAppConfigurationRetriever
{
    public static class AzureAppConfigurationRetriever
    {
        public static Hashtable GetConfigurationsByLabel(string endpoint, string label = "", bool mergeWithEmptyLabel = true)
        {
            var cred = new DefaultAzureCredential();
            var client = new ConfigurationClient(new Uri(endpoint), cred);

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

                retValue = Merge(labelSettingsDictionaryList);
            }
            else
            {
                retValue = labelSetting;
            }

            return new Hashtable(retValue);
        }
        private static Dictionary<K, V> Merge<K, V>(IEnumerable<Dictionary<K, V>> dictionaries) where K : notnull
        {
            if (dictionaries is null)
            {
                throw new ArgumentNullException(nameof(dictionaries));
            }

            Dictionary<K, V> result = new Dictionary<K, V>();

            foreach (Dictionary<K, V> dict in dictionaries)
            {
                return dictionaries.SelectMany(x => x)
                 .ToLookup(pair => pair.Key, pair => pair.Value)
                 .ToDictionary(g => g.Key, g => g.Last());
            }

            return result;
        }

    }
}