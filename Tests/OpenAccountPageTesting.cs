using demo.litecart.net_POM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Tests
{
    [TestFixture]
    public class OpenAccountPageTesting : TestBase
    {
        protected override string url => "https://demo.litecart.net/";
        [Test]
        public void WhenUserUnlogged_ClickOnAccountButton_ShouldOpenUnloggedAccountPage()
        {
            MainPage mainPage = new MainPage(driver);
            mainPage.OpenAccountPage();
            mainPage.CheckThatAllertMessageContainsText("You must be logged in to view the page.");
            string url = driver.Url;

            Assert.That(url, Does.Contain("/login?redirect_url="));
        }

        [Test]
        public void WhenUserLoggedIn_ClickOnAccountButton_ShouldOpenAccountPage()
        { 
            MainPage mainPage = new MainPage(driver);
            mainPage.LoginWithEmailAndPassword("user@email.com", "demo");
            mainPage.CheckThatAllertMessageContainsText("logged in as John Doe.");
            mainPage.OpenAccountPage();

            string url = driver.Url;

            Assert.That(url, Does.Contain("/edit_account"));
        }
    }
}
