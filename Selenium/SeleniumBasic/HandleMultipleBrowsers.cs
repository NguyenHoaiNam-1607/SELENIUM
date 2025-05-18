using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium
{
    public class HandleMultipleBrowsers
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
            TestContext.Out.WriteLine("---OpenBrowserTab---");
            driver.Url = "https://demoqa.com/browser-windows";

            string parentWindowHandle = driver.CurrentWindowHandle;
            var tabButton = driver.FindElement(By.Id("tabButton")); // New Tab button
            var windowButton = driver.FindElement(By.Id("windowButton")); // New Window button

            tabButton.Click();
            windowButton.Click();

            var windowHandles = driver.WindowHandles.ToList(); // Danh sách ID các Tab/Window đang mở
            windowHandles.Remove(parentWindowHandle);

            int num = 1;
            foreach (var windowHandle in windowHandles)
            {
                try
                {
                    TestContext.Out.WriteLine($"{num}. CurrentWindowHandle: {windowHandle} - {driver.Title}");
                    driver.SwitchTo().Window(windowHandle); // Chuyển Tab/Window
                    var sampleHeading = driver.FindElement(By.XPath("//*[@id=\"sampleHeading\"]"));
                    TestContext.Out.WriteLine(sampleHeading.Text);

                }
                catch (Exception ex)
                {
                    TestContext.Out.WriteLine("Error: " + ex.Message);
                }
                num++;
            }

            TestContext.Out.WriteLine($"{num}. ParentWindowHandle: {parentWindowHandle}");
            driver.SwitchTo().Window(parentWindowHandle); // Chuyển về parent Window
            var element = driver.FindElement(By.XPath("//h1[contains(text(),'Browser Windows')]"));
            TestContext.Out.WriteLine(element.Text);
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
