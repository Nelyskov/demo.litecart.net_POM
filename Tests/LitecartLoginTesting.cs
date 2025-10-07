using demo.litecart.net_POM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure;
using Allure.NUnit.Attributes;

namespace demo.litecart.net_POM.Tests
{
    [TestFixture]
    public class LitecartLoginTesting : TestBase
    {
        protected override string url => "https://demo.litecart.net/";

        [Test]
        [AllureDescription("Проверка успешного логирования пользователя с валидными данными")]
        public void WhenLoginWithValidNameAndPassword_SuccessMessageShouldAppear()
        { 
            MainPage mainPage = new MainPage(driver);
            mainPage.LoginWithEmailAndPassword("user@email.com", "demo");
            mainPage.CheckThatAllertMessageContainsText("logged in as John Doe.");
        }

        [Test]
        [AllureDescription("Проверка логирования пользователя с невалидными данными")]
        public void WhenLoginWithNotValidNameAndPassword_UnsuccessMessageShouldAppear()
        {
            MainPage mainPage = new MainPage(driver);
            mainPage.LoginWithEmailAndPassword("invalid@email.com", "incorrectpass");
            mainPage.CheckThatAllertMessageContainsText("The email does not exist in our database");
        }

    }
}
