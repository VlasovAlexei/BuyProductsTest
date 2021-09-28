using System;
using Xunit;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace FindElementsInChrome
{
    public class Page
    {
        public string Name { get; set; }
        public Dictionary<String, String> XpathElements;
    }

    public class SiteConfiguration
    {
        public string Url { get; set; }
        public List<Page> Pages;
    }

    public class TestsBase
    {
        [Theory]
        [InlineData(@"JsonFiles\CitilinkInfo.json")]
        [InlineData(@"JsonFiles\DNSInfo.json")]

        public static void ShopTest(string pathToJsonFile)
        {
            var siteConfiguration = JsonConvert.DeserializeObject<SiteConfiguration>(File.ReadAllText(pathToJsonFile));
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            IWebDriver driver = new ChromeDriver(options);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl(siteConfiguration.Url);
            driver.Manage().Window.Maximize();

            var productListPage = siteConfiguration.Pages.Where(p => p.Name == "ProductList").FirstOrDefault();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(productListPage.XpathElements["findFieldOfSearch"]))).SendKeys("iPhone 12");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(productListPage.XpathElements["findFieldOfSearch"]))).SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            var productDetalesPage = siteConfiguration.Pages.Where(p => p.Name == "ProductDetails").FirstOrDefault();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(productDetalesPage.XpathElements["productId"]))).Click();
            var expectedResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(productDetalesPage.XpathElements["nameOfProduct"]))).Text;
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(productDetalesPage.XpathElements["buyButton"]))).Click();
            Thread.Sleep(1000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(productDetalesPage.XpathElements["basketButton"]))).Click();

            var actualResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(productDetalesPage.XpathElements["nameOfProductInBasket"]))).Text;
            var actualCount = driver.FindElements(By.XPath(productDetalesPage.XpathElements["nameOfProductInBasket"])).Count;

            Assert.Contains(actualResult, expectedResult);
            Assert.Equal(1, actualCount);
            driver.Quit();
        }
    }
}