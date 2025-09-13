using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Pages
{
    public class CreateAccountPage : BasePage
    {
        /// <summary>
        /// Sign In form
        /// </summary>
        private By emailField => By.Name("email");
        private By passwordField => By.Name("password");
        private By rememberMeCheckBox => By.Name("remember_me");
        private By loginBtn => By.XPath("//button[@name='login' and contains(text(), 'Sign In')]");

        /// <summary>
        /// Create Account form
        /// </summary>
        private By companyField => By.Name("company");
        private By taxIDField => By.Name("tax_id");
        private By firstNameField => By.Name("firstname");
        private By lastNameField => By.Name("lastname");
        private By address1Field => By.Name("address1");
        private By address2Field => By.Name("address2");
        private By postalCodeField => By.Name("postcode");
        private By cityField => By.Name("city");
        private By countryDropdown => By.Name("country_code");
        private By zoneCodeField => By.Name("zone_code");
        private By emailFieldCreateAccountForm => By.XPath("//*[@id=\"box-create-account\"]/div[2]/form/div[6]/div[1]/div/input");
        private By phoneField => By.Name("phone");
        private By desiredPasswordField => By.XPath("//*[@id=\"box-create-account\"]/div[2]/form/div[7]/div[1]/div/input");
        private By confirmPasswordField => By.Name("confirmed_password");
        private By newsletterCheckBox => By.Name("newsletter");
        private By termsAgreedCheckBox => By.Name("terms_agreed");
        private By captchaField => By.Name("captcha");
        private By createAccountBtn => By.Name("create_account");

        public CreateAccountPage(IWebDriver driver) : base(driver) 
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
        public MainPage RegisterWithRequiredField(string firstname, string lastname, string country, string desiredpassword, string confirmpassword)
        { 
            WaitForElement(firstNameField).SendKeys(firstname);
            WaitForElement(lastNameField).SendKeys(lastname);

            var countrySelector = WaitForElement(countryDropdown);
            countrySelector.Click();
            countrySelector.SendKeys(country + Keys.Enter);

            WaitForElement(desiredPasswordField).SendKeys(desiredpassword);
            WaitForElement(confirmPasswordField).SendKeys(confirmpassword);

            WaitForCaptchInput();

            WaitForElement(termsAgreedCheckBox).Click();
            WaitForElement(createAccountBtn).Click();

            return new MainPage(driver);
        }
    }
}
