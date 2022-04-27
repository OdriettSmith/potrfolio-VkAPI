using Aquality.Selenium.Browsers;
using NUnit.Framework;
using TestVK.Utils;

namespace TestVK.Tests
{
    internal abstract class BaseTest
    {
        private static Browser browser = AqualityServices.Browser;

        [SetUp]
        public void BeforeTest()
        {
            browser.Maximize();
            browser.GoTo(JsonUtils.GetUrl().HomeUrl);
            browser.WaitForPageToLoad();
        }

        [TearDown]
        public void AfterTest()
        {
            browser.Quit();
        }
    }
}
