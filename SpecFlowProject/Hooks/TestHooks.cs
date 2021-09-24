using FindProductSpecFlow.Hooks;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Hooks
{
    [Binding]
    public class TestHooks
    {
        private DriverHelper _driverHelper;
        public TestHooks(DriverHelper driverHelper) => _driverHelper = driverHelper;

        [BeforeScenario]
        public void BeforeScenario()
        {
            // добавить чтение из json файла
            SiteSetting restoredProduct = await JsonSerializer.DeserializeAsync<Product>(fs);

            //1 вариант:в хуке создаем конфигурацию
            // почитать про теги сценариев и как для одного тега взять один json, а для другого другой - specflow документация
            //метод GetTag, 


            ChromeOptions option = new ChromeOptions();
            option.AddArguments("start-maximized");
            option.AddArguments("--disable-notifications");
            _driverHelper.Driver = new ChromeDriver(option);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driverHelper.Driver.Quit();
        }
    }
}
