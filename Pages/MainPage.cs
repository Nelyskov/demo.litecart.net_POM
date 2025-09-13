using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V137.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Pages
{
    public class MainPage : BasePage
    {
        private By searchField => By.Name("query");
        private By accountBtn => By.CssSelector("a.account.text-center");
        private By cartBtn => By.Id("cart");
        private By signInDropdownMenuBtn => By.XPath("//*[@id=\"offcanvas\"]/div[2]/ul[2]/li[3]");
        private By emailField => By.XPath("//*[@id=\"offcanvas\"]/div[2]/ul[2]/li[3]/ul/li[1]/form/div[1]/div/input");
        private By passwordField => By.Name("password");
        private By rememberMeCheckBox => By.Name("remember_me");
        private By loginBtn => By.XPath("//button[@name='login' and contains(text(), 'Sign In')]");
        private By alertMessage => By.CssSelector("#notices .alert");
        private By subscribeEmailField => By.CssSelector("#box-newsletter-subscribe input[name='email']");
        private By subscribeBtn => By.Name("subscribe");
        private By termsAgreeCheckBox => By.Name("terms_agreed");
        private By userMenuDropdown => By.CssSelector("#offcanvas .dropdown-toggle");


        public MainPage(IWebDriver driver) : base(driver) 
        {
        }
        public SearchResultsPage Search(string input)
        { 
            WaitForElement(searchField).SendKeys(input + Keys.Enter);
            return new SearchResultsPage(driver);
        }
        public MainPage LoginWithEmailAndPassword(string email, string password)
        {
            WaitForElement(signInDropdownMenuBtn).Click();

            // !!! ИСПРАВИТЬ!!!
            Thread.Sleep(1000); 

            var emailField = WaitForElement(this.emailField);
            emailField.Clear();
            emailField.SendKeys(email);

            var passwordField = WaitForElement(this.passwordField);
            passwordField.Clear();
            passwordField.SendKeys(password);

            WaitForElement(rememberMeCheckBox).Click();
            WaitForElement(loginBtn).Click();

            WaitForPageLoad();
            return new MainPage(driver);    
        }
        public MainPage Subscribe(string email)
        {
            ScrollToBottom();
            var emailField = WaitForElement(subscribeEmailField);
            emailField.Clear();
            emailField.SendKeys(email);

            WaitForElement(termsAgreeCheckBox).Click();
            WaitForElement(subscribeBtn).Click();

            WaitForPageLoad();
            return new MainPage(driver);
        }

        public BasePage OpenAccountPage()
        { 
            return IsUserLoggedIn() ? new AccountPage(driver) : new UnloggedAccountPage(driver);
        }

        public CartPage OpenCart()
        {
            WaitForElement(cartBtn).Click();
            WaitForPageLoad();
            return new CartPage(driver);
        }

        public void CheckThatAllertMessageContainsText(string message)
        {
            Assert.That(WaitForElement(alertMessage).Text.Contains(message));
        }

    }
}
