using demo.litecart.net_POM.ElementsInfo;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.Tests
{
    public class GetAllPageButtons : TestBase
    {
        protected override string url => "https://demo.litecart.net/";
        string type = "button";
        string path = "D:\\Git\\demo.litecart.net_POM\\Lists Of Elements on page";

        [Test]
        public void WriteAllElements()
        {
            var writer = new ElementsWriter();
            writer.WriteAllElementsByTypeToJson(driver, type, path);

        }

    }
}
