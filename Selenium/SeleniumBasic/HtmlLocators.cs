using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Selenium;

public class HtmlLocators
{
    IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
        driver = new ChromeDriver(chromeOptions);
        driver.Manage().Window.Maximize();
        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);     //  Chờ tối đa 10s khi một Element chưa xuất hiện ngay lập tức
    }

    [Test]
    public void Test1()
    {
        driver.Url = "https://demoqa.com/automation-practice-form";

        IWebElement element;

        //By Id 
        element = driver.FindElement(By.Id("userEmail"));   // => document.getElementById("userEmail")
        TestContext.Out.WriteLine($"- By Id:  {getOuterHTML(element)}");

        //By Name
        element = driver.FindElement(By.Name("gender"));    // => document.getElementsByName("gender")[0]
        TestContext.Out.WriteLine($"- By Name:  {getOuterHTML(element)}");

        //By CssSelector
        element = driver.FindElement(By.CssSelector("#userNumber")); // => document.querySelector("#userNumber")
        TestContext.Out.WriteLine($"- By CssSelector:  {getOuterHTML(element)}");

        //By ClassName
        element = driver.FindElement(By.ClassName("practice-form-wrapper")); // => document.getElementsByClassName("practice-form-wrapper")[0]
        TestContext.Out.WriteLine($"- By ClassName:  {getOuterHTML(element)}");

        //By TagName
        element = driver.FindElement(By.TagName("form"));  // => document.getElementsByTagName("form")[0]
        TestContext.Out.WriteLine($"- By TagName:  {getOuterHTML(element)}");

        //Effective XPath
        // https://quickref.me/xpath.html
        element = driver.FindElement(By.XPath("//input[@id=\"firstName\"]"));  // => $x("//input[@id=\"firstName\"]")[0]
        TestContext.Out.WriteLine($"- By XPath Tag + Attribute:  {getOuterHTML(element)}");

        //Contains
        element = driver.FindElement(By.XPath("//input[contains(@id,\"last\")]"));
        TestContext.Out.WriteLine($"- By XPath Contains:  {getOuterHTML(element)}");

        //Starts-with
        element = driver.FindElement(By.XPath("//input[starts-with(@placeholder,\"First\")]"));
        TestContext.Out.WriteLine($"- By XPath Starts-with:  {getOuterHTML(element)}");

        //Or
        element = driver.FindElement(By.XPath("//label[text()='Select picture' or starts-with(text(), 'Select')]"));
        TestContext.Out.WriteLine($"- By XPath Starts-with + or:  {getOuterHTML(element)}");
        //And 
        element = driver.FindElement(By.XPath("//label[text()='Date of Birth' and starts-with(text(), 'Date')]"));
        TestContext.Out.WriteLine($"- By XPath Starts-with + and:  {getOuterHTML(element)}");

        //Ancestor - Dùng để truy ngược các Element(Tổ tiên) chứa Element hiện tại. Dùng để xử lý các DOM phức tạp mà không có ID/Name/Class.. rõ ràng
        element = driver.FindElement(By.XPath("//*[@id=\"firstName\"]//ancestor::div[2]"));
        TestContext.Out.WriteLine($"- By XPath Ancestor:  {getOuterHTML(element)}");

        //Child - Dùng để chọn các Element con của Element hiện tại 
        element = driver.FindElement(By.XPath("//*[@id=\"userName-wrapper\"]//child::div[1]"));
        TestContext.Out.WriteLine($"- By XPath Child:  {getOuterHTML(element)}");

        //Following - Dùng để chọn các Element xuất hiện sau Element hiện tại
        element = driver.FindElement(By.XPath("//*[@id=\"firstName\"]//following::div[1]"));
        TestContext.Out.WriteLine($"- By XPath Following:  {getOuterHTML(element)}");

        //Preceding - Dùng để chọn các Element xuất hiện trước Element hiện tại
        element = driver.FindElement(By.XPath("//*[@id=\"firstName\"]//preceding::div[1]"));
        TestContext.Out.WriteLine($"- By XPath Preceding:  {getOuterHTML(element)}");

        //Following-sibling - Dùng để chọn các Element (anh/em) phía sau Element hiện tại 
        element = driver.FindElement(By.XPath("//*[@id=\"userName-wrapper\"]//div[1]//following-sibling::div[1]"));
        TestContext.Out.WriteLine($"- By XPath Following-sibling:  {getOuterHTML(element)}");

        //Descendant - Dùng để chọn tất cả các Element(Con/cháu...) bên trong element hiện tại
        element = driver.FindElement(By.XPath("//*[@id=\"userName-wrapper\"]//div[1]//descendant::*"));
        TestContext.Out.WriteLine($"- By XPath Descendant:  {getOuterHTML(element)}");

        //Parent - Dùng để chọn Element cha của Element hiện tại
        element = driver.FindElement(By.XPath("//*[@id=\"firstName\"]//parent::div"));
        TestContext.Out.WriteLine($"- By XPath Parent:  {getOuterHTML(element)}");

        //Locate an Element inside Array of Elements
        List<IWebElement>? elements = new List<IWebElement>();
        elements = driver.FindElements(By.XPath("//input[starts-with(@id,\"hobbies-checkbox\")]"))?.ToList();
        if(elements != null)
        {
            foreach (var item in elements)
            {
                TestContext.Out.WriteLine($"- By XPath Element {elements.IndexOf(item) + 1} in Array:  {getOuterHTML(item)}");
            }
        }

        //By LinkText
        driver.Navigate().GoToUrl("https://demoqa.com/links");
        element = driver.FindElement(By.LinkText("Home"));
        TestContext.Out.WriteLine($"- By LinkText:  {getOuterHTML(element)} - {element.GetAttribute("href")}");

        //By PartialLinkText
        element = driver.FindElement(By.PartialLinkText("Content"));
        TestContext.Out.WriteLine($"- By PartialLinkText:  {getOuterHTML(element)} - {element.GetAttribute("href")}");
    }

    [TearDown]
    public void Teardown()
    {
        Thread.Sleep(TimeSpan.FromSeconds(3));
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    }

    public string getOuterHTML(IWebElement element)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        var outerHtml = js.ExecuteScript("return arguments[0].outerHTML;", element);
        return outerHtml != null ? (string)outerHtml : "";
    }
}
