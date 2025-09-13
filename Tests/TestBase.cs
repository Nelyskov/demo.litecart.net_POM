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
            driver = new ChromeDriver();
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
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
            driver.Close();
        }
    }
}
