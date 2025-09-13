using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Pages
{
    public class AccountPage : BasePage
    {
        private By message => By.XPath("//*[@id=\"notices\"]");
        private By dropDownMune => By.XPath("//*[@id=\"offcanvas\"]/div[2]/ul[2]/li[3]/a");
        private By dropDownMenuText => By.XPath("//*[@id=\"offcanvas\"]/div[2]/ul[2]/li[3]/a/i"); // Если Sign In - пользователь не зарегистрирован, если John - пользователь авторизован
        public AccountPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsUserLoggedIn_CheckByDropDownMenu()
        {
            return WaitForElement(dropDownMenuText).Text != "Sign in";
        }

        public bool IsUSerLoggedIn_CheckByPageTitle()
        {
            return !driver.Title.Contains("Sign In");
        }
    }
}
