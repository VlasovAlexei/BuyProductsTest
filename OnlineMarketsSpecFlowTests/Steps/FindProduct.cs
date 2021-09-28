using FindProductSpecFlow.Hooks;
using OpenQA.Selenium;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlowProject.Steps
{
    [Binding]

    public class FindProducts
    {
        private DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        public FindProducts(DriverHelper driverHelper, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"I am open site with url ""(.*)""")]
        public void GivenIAmOpenSiteWithUrl(string url)
        {
            _driverHelper.Driver.Navigate().GoToUrl(url);
        }

        [When(@"I selected ""(.*)"" and find product by name ""(.*)""")]
        public void WhenISelectedAndFindProductByName(string findFieldOfSearch, string findProductName)
        {
            _driverHelper.Driver.FindElement(By.XPath(findFieldOfSearch)).SendKeys(findProductName);
        }

        [When(@"I click on product ""(.*)""")]
        public void WhenIClickOnProduct(string productId)
        {
            _driverHelper.Driver.FindElement(By.XPath(productId));
            Thread.Sleep(6000);
            _driverHelper.Driver.FindElement(By.XPath(productId)).Click();
        }

        [When(@"I get product name from page and put it in scenario context by name '(.*)'")]
        public void WhenIGetProductNameFromPageAndPutItInScenarioContextByName(string _nameOfProduct)
        {
            // заменить xpath, брать из json используя scenario context 
            var expectedResult = _driverHelper.Driver.FindElement(By.XPath("//h1[contains(text(),'iPhone 12')]")).Text;
            Thread.Sleep(6000);
            _scenarioContext[_nameOfProduct] = expectedResult;
        }


        [When(@"I click on ""(.*)""")]
        public void WhenIClickOn(string buyButton)
        {
            _driverHelper.Driver.FindElement(By.XPath(buyButton)).Click();
            Thread.Sleep(6000);
        }

        [When(@"I click on  ""(.*)"" button")]
        public void WhenIClickOnButton(string basketButton)
        {
            _driverHelper.Driver.FindElement(By.XPath(basketButton)).Click();
            Thread.Sleep(6000);
        }

        [Then(@"I validate ""(.*)"" the only one")]
        public void ThenIValidateTheOnlyOne(string nameOfProductInBasket)
        {
            var actualCount = _driverHelper.Driver.FindElements(By.XPath(nameOfProductInBasket)).Count;
            Assert.Equal(1, actualCount);
        }

        [Then(@"I get product name from basket page and put it in scenario context by name '(.*)'")]
        public void ThenIGetProductNameFromBasketPageAndPutItInScenarioContextByName(string _nameOfProductInBasket)
        {
            var actualResult = _driverHelper.Driver.FindElement(By.XPath("//a[@class='ProductCardForBasket__name  Link js--Link Link_type_default'][contains(text(), 'iPhone 12')]")).Text;
            _scenarioContext[_nameOfProductInBasket] = actualResult;
        }

        [Then(@"I validate two scenario contexts have equal text '(.*)', '(.*)'")]
        public void ThenIValidateTwoScenarioContextsHaveEqualText(string _expectedResult, string _actualResult)
        {
            var expectedResult = (string)_scenarioContext[_expectedResult];
            var actualResult = (string)_scenarioContext[_actualResult];
            Assert.Contains(expectedResult, actualResult);
        }

    }
}