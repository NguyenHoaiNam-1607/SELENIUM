using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    public class SelectOption
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://demoqa.com/select-menu";

            var select = driver.FindElement(By.XPath("//select[@id=\"oldSelectMenu\"]"));
            // 
            SelectElement oSelect = new SelectElement(select);  // "Install-Package Selenium.Support" or "dotnet add package Selenium.Support"
            var options = oSelect.Options;
            TestContext.Out.WriteLine("Old Style Select Menu: " + options.Count);
            foreach (var o in options)
            {
                TestContext.Out.WriteLine($"- Text: {o.Text}; Value: {o.GetAttribute("value")}");
            }
            oSelect.SelectByIndex(3);

            var multiSelect = driver.FindElement(By.XPath("//select[@id=\"cars\"]"));
            var oMultiSelect = new SelectElement(multiSelect);
            var optionsMultiSelect = oMultiSelect.Options;
            TestContext.Out.WriteLine("Standard multi select: " + optionsMultiSelect.Count);
            foreach (var o in optionsMultiSelect)
            {
                TestContext.Out.WriteLine($"- Text: {o.Text}; Value: {o.GetAttribute("value")}");
            }

            bool isMultiple = oMultiSelect.IsMultiple;
            if (isMultiple)
            {
                oMultiSelect.SelectByIndex(0);
                oMultiSelect.SelectByIndex(1);
                oMultiSelect.SelectByIndex(2);
                Thread.Sleep(TimeSpan.FromSeconds(3));
                oMultiSelect.DeselectByIndex(1);
            }
        }

        [TearDown]
        public void Teardown()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
