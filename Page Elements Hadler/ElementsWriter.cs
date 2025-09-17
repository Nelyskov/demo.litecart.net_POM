using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace demo.litecart.net_POM.ElementsInfo
{
    /// <summary>
    /// Write all elements to .json file
    /// To use you should create new class ElementWriter
    /// </summary>
    public class ElementsWriter
    {

        public void WriteAllElementsByTypeToJson(IWebDriver driver, string typeOfElement, string path)
        {
            var elements = driver.FindElements(By.TagName(typeOfElement)).ToList();
            var url = driver.Url;

            var elementObjects = elements.Select(e =>
            {
                string text = string.IsNullOrWhiteSpace(e.Text) ? e.GetAttribute("value") : e.Text;
                string id = e.GetAttribute("id");
                string className = e.GetAttribute("class");
                string name = e.GetAttribute("name");
                string value = e.GetAttribute("value");
                string type = e.GetAttribute("type");

                //string cssSelector = !string.IsNullOrEmpty(id) ? $"#{id}" :
                //                     !string.IsNullOrEmpty(className) ? $"{typeOfElement}.{className.Replace(" ", ".")}" : typeOfElement;
                //string xPath = $"//{typeOfElement}";
                //if (!string.IsNullOrEmpty(id))
                //{
                //    xPath += $"[@id='{id}']";
                //} 
                //else if (!string.IsNullOrEmpty(name))
                //{ 
                //    xPath += $"[@name='{name}']"; 
                //}

                string xPath = GetElementXPath(driver, e);
                string cssSelector = GetElementCssSelector(driver, e);

                return new ElementBaseClass(
                    text, id, className, name, value, type, xPath, cssSelector, url
                );

            }).ToList();

            string filePath = Path.Combine(path, $"{typeOfElement}.json");

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(elementObjects, options);

            File.WriteAllText(filePath, json);

            Console.WriteLine($"Found {elements.Count} elements <{typeOfElement}>. File saved: {filePath}");
        }

        private string GetElementXPath(IWebDriver driver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)driver;
            string script = @"
            function getElementXPath(elt) {
                var path = '';
                for (; elt && elt.nodeType == 1; elt = elt.parentNode) {
                    var idx = 1;
                    for (var sib = elt.previousSibling; sib; sib = sib.previousSibling) {
                        if (sib.nodeType == 1 && sib.tagName == elt.tagName) idx++;
                    }
                    var xname = elt.tagName.toLowerCase();
                    path = '/' + xname + '[' + idx + ']' + path;
                }
                return path;
            }
            return getElementXPath(arguments[0]);
        ";
            return (string)js.ExecuteScript(script, element);
        }
        private string GetElementCssSelector(IWebDriver driver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)driver;
            string script = @"
            function getCssSelector(el) {
                if (!(el instanceof Element)) return;
                var path = [];
                while (el.nodeType === Node.ELEMENT_NODE) {
                    var selector = el.nodeName.toLowerCase();
                    if (el.id) {
                        selector += '#' + el.id;
                        path.unshift(selector);
                        break;
                    } else {
                        var sib = el, nth = 1;
                        while (sib = sib.previousElementSibling) {
                            if (sib.nodeName.toLowerCase() == selector) nth++;
                        }
                        selector += ':nth-of-type(' + nth + ')';
                    }
                    path.unshift(selector);
                    el = el.parentNode;
                }
                return path.join(' > ');
            }
            return getCssSelector(arguments[0]);
        ";
            return (string)js.ExecuteScript(script, element);
        }
    }
}
