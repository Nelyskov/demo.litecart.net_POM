using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace demo.litecart.net_POM.ElementsInfo
{
    public class ElementBaseClass
    {
        public string Text { get; set; }
        public string Id { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string XPath { get; set; }
        public string CssSelector { get; set; }
        public string Url { get; set; }

        public ElementBaseClass(
            string text, string id, string className, string name, string value,
            string type, string xPath, string cssSelector, string url)
        {
            Text = text;
            Id = id;
            ClassName = className;
            Name = name;
            Value = value;
            Type = type;
            XPath = xPath;
            CssSelector = cssSelector;
            Url = url;
        }
    }
}
