using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    public class SwitchCommand
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void Test1()
        {
            OpenJavaScriptAlert();
            OpenBrowserTab();
            OpenNewBrowserWindow();
            SwitchToFrames();
            SwitchToNestedFrames();
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

        public void OpenJavaScriptAlert()
        {
            TestContext.Out.WriteLine("---OpenJavaScriptAlert---");
            driver.Url = "https://demoqa.com/alerts";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Explicit waits

            // alert
            var alertButton = driver.FindElement(By.Id("alertButton"));
            alertButton.Click();
            var alert = driver.SwitchTo().Alert();
            TestContext.Out.WriteLine("1. alert " + alert.Text);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            alert.Accept();

            // after 5 seconds
            var alertAfter5sButton = driver.FindElement(By.Id("timerAlertButton"));
            alertAfter5sButton.Click();
            Thread.Sleep(TimeSpan.FromSeconds(5));
            var alertAfter5s = driver.SwitchTo().Alert();
            TestContext.Out.WriteLine("2. after 5 seconds " + alertAfter5s.Text);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            alertAfter5s.Accept();

            // confirm box
            var confirmButton = driver.FindElement(By.Id("confirmButton"));
            confirmButton.Click();
            var confirm = driver.SwitchTo().Alert();
            TestContext.Out.WriteLine("3. confirm box " + confirm.Text);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            alert.Dismiss();
            var confirmResult = driver.FindElement(By.XPath("//*[starts-with(text(),'You selected')]"));
            TestContext.Out.WriteLine(confirmResult.Text);

            // prompt box
            var promptButton = driver.FindElement(By.Id("promtButton"));
            promptButton.Click();
            var prompt = WaitForAlert(wait);
            TestContext.Out.WriteLine("4. prompt box " + prompt.Text);
            prompt.SendKeys("Nguyen Van A");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            alert.Accept();
            var promptResult = driver.FindElement(By.XPath("//*[starts-with(text(),'You entered')]"));
            TestContext.Out.WriteLine(promptResult.Text);
        }

        public void OpenBrowserTab()
        {
            driver.Url = "https://demoqa.com/browser-windows";

            var tabButton = driver.FindElement(By.Id("tabButton"));
            tabButton.Click();

            var windowsHandle = driver.WindowHandles.ToList();
            TestContext.Out.WriteLine("Windows count: " + windowsHandle.Count);

            var newTabHandle = driver.WindowHandles.Last();
            driver.SwitchTo().Window(newTabHandle);
            var newTabText = driver.FindElement(By.XPath("//h1[@id='sampleHeading']")).Text;
            TestContext.Out.WriteLine(newTabText);

            string originalTabHandle = driver.WindowHandles.First();
            driver.SwitchTo().Window(originalTabHandle);
            TestContext.Out.WriteLine(originalTabHandle);

            var originalTab = driver.FindElement(By.XPath("//h1[contains(text(), 'Browser Windows')]")).Text;
            TestContext.Out.WriteLine(originalTab);
        }

        public void OpenNewBrowserWindow()
        {
            TestContext.Out.WriteLine("-- OpenNewBrowserWindow --");
            driver.Url = "https://demoqa.com/browser-windows";
            var windowButton = driver.FindElement(By.Id("windowButton"));
            windowButton.Click();

            var windowsHandle = driver.WindowHandles.ToList();
            TestContext.Out.WriteLine("Windows count: " + windowsHandle.Count);

            var newWindowHandle = driver.WindowHandles.Last();
            TestContext.Out.WriteLine(newWindowHandle);
            driver.SwitchTo().Window(newWindowHandle);
            var newWindowText = driver.FindElement(By.XPath("//h1[@id='sampleHeading']")).Text;
            TestContext.Out.WriteLine(newWindowText);

            string originalTabHandle = driver.WindowHandles.First();
            driver.SwitchTo().Window(originalTabHandle);
            TestContext.Out.WriteLine(originalTabHandle);

            var originalTab = driver.FindElement(By.XPath("//h1[contains(text(), 'Browser Windows')]")).Text;
            TestContext.Out.WriteLine(originalTab);
        }

        public void OpenNewBrowserWindowMessage()
        {
            TestContext.Out.WriteLine("-- OpenNewBrowserWindowMessage --");
            driver.Url = "https://demoqa.com/browser-windows";
            var messageWindowButton = driver.FindElement(By.Id("messageWindowButton"));
            messageWindowButton.Click();

            var windowsHandle = driver.WindowHandles.ToList();
            TestContext.Out.WriteLine("Windows count: " + windowsHandle.Count);

            var newWindowHandle = driver.WindowHandles.Last();
            TestContext.Out.WriteLine(newWindowHandle);
            driver.SwitchTo().Window(newWindowHandle);
            var newWindowTextAlert = driver.FindElement(By.XPath("//h1[@id='sampleHeading']")).Text;
            TestContext.Out.WriteLine(newWindowTextAlert);

            string originalTabHandle = driver.WindowHandles.First();
            driver.SwitchTo().Window(originalTabHandle);
            TestContext.Out.WriteLine(originalTabHandle);

            var originalTab = driver.FindElement(By.XPath("//h1[contains(text(), 'Browser Windows')]")).Text;
            TestContext.Out.WriteLine(originalTab);
        }

        public void SwitchToFrames()
        {
            TestContext.Out.WriteLine("-- SwitchToIframes --");
            driver.Url = "https://demoqa.com/frames";

            var frame1 = driver.FindElement(By.XPath("//iframe[@id='frame1']"));
            driver.SwitchTo().Frame(frame1);
            var frame1Text = driver.FindElement(By.XPath("//h1[@id='sampleHeading']")).Text;
            TestContext.Out.WriteLine("In Frame 1: " + frame1Text);
            driver.SwitchTo().DefaultContent();  // Thoát khỏi Frame chuyển về Main Page

            var frame2 = driver.FindElement(By.Id("frame2"));
            driver.SwitchTo().Frame(frame2);
            var frame2Text = driver.FindElement(By.XPath("//h1[@id='sampleHeading']")).Text;
            TestContext.Out.WriteLine("In Frame 2: " + frame2Text);
            driver.SwitchTo().DefaultContent();
        }
        public void SwitchToNestedFrames()
        {
            TestContext.Out.WriteLine("-- switchToFrames --");
            driver.Url = "https://demoqa.com/nestedframes";

            var parentFrame = driver.FindElement(By.XPath("//iframe[@id='frame1']"));
            driver.SwitchTo().Frame(parentFrame);
            var element = driver.FindElement(By.XPath("//body"));
            TestContext.Out.WriteLine("In Parent Frame: " + element.Text);

            var childFrame = driver.FindElement(By.XPath("//iframe[1]"));
            driver.SwitchTo().Frame(childFrame);
            element = driver.FindElement(By.XPath("/html/body/p"));
            TestContext.Out.WriteLine("In Child Frame: " + element.Text);

            driver.SwitchTo().DefaultContent();
            element = driver.FindElement(
                By.XPath("//*[starts-with(@id,'framesWrapper')]/div[starts-with(text(), 'Sample Nested Iframe')]"));
            TestContext.Out.WriteLine("Switch back : " + element.Text);
        }



        public IAlert WaitForAlert(WebDriverWait wait)
        {
            return wait.Until(driver =>
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            });
        }
    }
}
