using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Pages
{
    public class UnloggedAccountPage : BasePage
    {
        /// <summary>
        /// Sign In form
        /// </summary>
        private By alertMessage => By.XPath("/div/text()");
        private By emailField => By.Name("email");
        private By passwordField => By.Name("password");
        private By rememberMeCheckBox => By.Name("remember_me");
        private By loginBtn => By.XPath("//button[@name='login' and contains(text(), 'Sign In')]");

        /// <summary>
        /// Creat An Account form
        /// </summary>
        private By registerNowBtn => By.XPath("//*[@id=\"box-login-create\"]/div[2]/p[1]/a");
        public UnloggedAccountPage(IWebDriver driver) : base(driver) 
        {
        
        }

        public AccountPage LogInWithEmailAndPassword(string email, string password)
        {
            var emailField = WaitForElement(this.emailField);
            emailField.Clear();
            emailField.SendKeys(email);

            var passwordField = WaitForElement(this.passwordField);
            passwordField.Clear();
            passwordField.SendKeys(email);

            WaitForElement(rememberMeCheckBox).Click();
            WaitForElement(loginBtn).Click();

            WaitForPageLoad();
            return new AccountPage(driver);
        }

        
    }
}
