using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        private IJavaScriptExecutor js;
        private By dropDownMenuText => By.CssSelector("#offcanvas .dropdown-toggle");

        protected By alertMessage => By.Id("notices");
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            js = (IJavaScriptExecutor)driver;
        }

        public void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public IWebElement WaitForElement(By locator)
        {
            return wait.Until(e => e.FindElement(locator));
        }

        public void ScrollToBottom()
        {
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }
        public void ScrollToTop()
        {
            js.ExecuteScript("window.scrollTo(0, 0)");
        }

        protected void WaitForPageLoad(int timeoutSeconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public bool IsUserLoggedIn()
        {
            var text = WaitForElement(dropDownMenuText).Text.Trim();
            return !text.Equals("Sign in", StringComparison.OrdinalIgnoreCase);
        }

        public void WaitForCaptchInput()
        {
            Console.WriteLine("MANUAL CAPTCHA");
            Console.WriteLine("Press ENTER to continue after entering captcha...");
            Console.ReadLine();
        }
    }
}
