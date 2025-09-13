using demo.litecart.net_POM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Tests
{
    [TestFixture]
    public class LitecartLoginTesting : TestBase
    {
        protected override string url => "https://demo.litecart.net/";

        [Test]
        public void WhenLoginWithValidNameAndPassword_SuccessMessageShouldAppear()
        { 
            MainPage mainPage = new MainPage(driver);
            mainPage.LoginWithEmailAndPassword("user@email.com", "demo");
            mainPage.CheckThatAllertMessageContainsText("logged in as John Doe.");
        }

        [Test]
        public void WhenLoginWithNotValidNameAndPassword_UnsuccessMessageShouldAppear()
        {
            MainPage mainPage = new MainPage(driver);
            mainPage.LoginWithEmailAndPassword("invalid@email.com", "incorrectpass");
            mainPage.CheckThatAllertMessageContainsText("The email does not exist in our database");
        }
    }
}
