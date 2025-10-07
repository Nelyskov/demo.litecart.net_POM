using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace demo.litecart.net_POM.Tests
{
    public abstract class TestBase
    {
        protected IWebDriver driver;
        //private WebDriverWait wait;
        protected abstract string url { get; }

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new"); // Включаем headless режим
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--remote-debugging-port=9222");
            options.AddArgument($"--user-data-dir=/tmp/chrome-user-data-{Guid.NewGuid()}"); // уникальная папка

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        public void WriteAllElementsByType(string typeOfElement, string path, bool append)
        {
            var elements = driver.FindElements(By.TagName(typeOfElement)).ToList();


            string filePath = Path.Combine(path, $"{typeOfElement}.json");

            using (StreamWriter writer = new StreamWriter(filePath, append))
            {
                foreach (var element in elements)
                {
                    string text = element.Text;
                    string id = element.GetAttribute("id");
                    string cssClass = element.GetAttribute("class");
                    string value = element.GetAttribute("value");
                    string type = element.GetAttribute("type");
                    string url = driver.Url;

                    string line = $"{{ \"text\": \"{text}\", \"id\": \"{id}\", \"class\": \"{cssClass}\", \"value\": \"{value}\", \"type\": \"{type}\", \"url\": \"{url}\" }}";
                    writer.WriteLine(line);
                }
            }
        }

        [TearDown]
        public void TearDown()
        {   
            //if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            //{
            //    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            //    var filePath = Path.Combine("/home/runner/work/demo.litecart.net_POM/demo.litecart.net_POM", $"{TestContext.CurrentContext.Test.Name}.png");
            //    screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            //    TestContext.AddTestAttachment(filePath);
            //}
            driver.Dispose();
            driver?.Quit();
        }
    }
}
