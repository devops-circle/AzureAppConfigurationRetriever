using System;
using System.Net;
using System.Security;
using Xunit;

namespace AzureAppConfigurationRetriever.Core.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void TestConvertToUnsecureString()
        {
            string stringTosecure = "thisisabigsecret'";

            SecureString theSecureString = new NetworkCredential("", stringTosecure).SecurePassword;

            Assert.Equal(stringTosecure, theSecureString.ConvertToUnsecureString());
        }

        [Fact]
        public void TestConvertToUnsecureStringNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => Extensions.ConvertToUnsecureString(null));
        }

    }
}
