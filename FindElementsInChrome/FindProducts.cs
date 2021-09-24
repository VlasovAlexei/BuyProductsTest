using System;
using Xunit;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Text.Json;
namespace FindElementsInChrome
{
    class Product
    {
        public string url { get; set; }
        public string findFieldOfSearch { get; set; }
        public string findProductName { get; set; }
        public string productId { get; set; }
        public string nameOfProduct { get; set; }
        public string buyButton { get; set; }
        public string basketButton { get; set; }
        public string nameOfProductInBasket { get; set; }
    }

    public class TestsBase
    {

        [Theory]
        [InlineData(@"CitilinkInfo.json")]
        [InlineData(@"DNSInfo.json")]

        public async void ShopTest(string pathToJsonFile)
        {
            using (FileStream fs = new FileStream(pathToJsonFile, FileMode.OpenOrCreate))
            {
                Product restoredProduct = await JsonSerializer.DeserializeAsync<Product>(fs);
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-notifications");
                IWebDriver driver = new ChromeDriver(options);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                driver.Navigate().GoToUrl(restoredProduct.url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.XPath(restoredProduct.findFieldOfSearch)).SendKeys(restoredProduct.findProductName);
                driver.FindElement(By.XPath(restoredProduct.findFieldOfSearch)).SendKeys(Keys.Enter);
                driver.FindElement(By.XPath(restoredProduct.productId));
                Thread.Sleep(6000);
                driver.FindElement(By.XPath(restoredProduct.productId)).Click();
                var expectedResult = driver.FindElement(By.XPath(restoredProduct.nameOfProduct)).Text;
                Thread.Sleep(6000);
                driver.FindElement(By.XPath(restoredProduct.buyButton)).Click();
                Thread.Sleep(6000);
                driver.FindElement(By.XPath(restoredProduct.basketButton)).Click();
                Thread.Sleep(6000);
                var actualResult = driver.FindElement(By.XPath(restoredProduct.nameOfProductInBasket)).Text;
                var actualCount = driver.FindElements(By.XPath(restoredProduct.nameOfProductInBasket)).Count;

                Assert.Contains(actualResult, expectedResult);
                Assert.Equal(1, actualCount);
                driver.Quit();
            }
        }
    }
}