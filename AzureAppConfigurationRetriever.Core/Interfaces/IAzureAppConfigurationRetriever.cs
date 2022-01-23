using System.Collections;

namespace AzureAppConfigurationRetriever.Core.Interfaces
{
    public interface IAzureAppConfigurationRetriever
    {
        Hashtable GetConfigurationsByLabel(string label = "", bool mergeWithEmptyLabel = true);

        string GetConfiguration(string valueName, string label = "");
    }
}