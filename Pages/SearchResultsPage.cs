using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Pages
{
    public class SearchResultsPage : BasePage
    {
        private By searchResultTitle => By.ClassName("card-title");
        public SearchResultsPage(IWebDriver driver) : base(driver) 
        {
        
        }

        public SearchResultsPage GetSearchResultsPage()
        {
            return this;
        }
    }
}
