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
                string cssSelector = !string.IsNullOrEmpty(id) ? $"#{id}" :
                                     !string.IsNullOrEmpty(className) ? $"{typeOfElement}.{className.Replace(" ", ".")}" : typeOfElement;
                string xPath = $"//{typeOfElement}";
                if (!string.IsNullOrEmpty(id))
                {
                    xPath += $"[@id='{id}']";
                } 
                else if (!string.IsNullOrEmpty(name))
                { 
                    xPath += $"[@name='{name}']"; 
                }

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
    }
}
