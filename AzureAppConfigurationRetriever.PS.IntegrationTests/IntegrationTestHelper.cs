using System;
using System.IO;

namespace AzureAppConfigurationRetriever.PS.IntegrationTests
{
    internal class IntegrationTestHelper
    {
        public static string GetModulePath()
        {
            string workingDirectory = Environment.CurrentDirectory;

            string path = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;

            path = Path.Combine(path, @"out\AzureAppConfigurationRetriever\AzureAppConfigurationRetriever.PS.dll");

            return path;
        }
    }
}
