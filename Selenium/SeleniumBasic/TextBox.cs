using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium
{
    public class TextBoxTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            // https://peter.sh/experiments/chromium-command-line-switches/ | https://github.com/GoogleChrome/chrome-launcher/blob/main/docs/chrome-flags-for-tools.md
            //chromeOptions.AddArgument("--headless");
            //chromeOptions.AddArgument("--window-size=1920x1080");
            //chromeOptions.AddArgument("--disable-gpu");
            //chromeOptions.AddArgument("--disable-extensions");
            //chromeOptions.AddArgument("proxy-server='direct://'");
            //chromeOptions.AddArgument("--proxy-bypass-list=*");
            //chromeOptions.AddArgument("--start-maximized");
            //chromeOptions.AddArgument("--disable-dev-shm-usage");
            //chromeOptions.AddArgument("--no-sandbox");
            //chromeOptions.AddArgument("--ignore-certificate-errors");
            //chromeOptions.AddArgument("--allow-running-insecure-localhost");
            //chromeOptions.AddArgument("--log-level=3");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            // Truy cập vào trang web
            driver.Url = "https://demoqa.com/"; 

            // Tìm kiếm element có chứa text và Click 
            IWebElement elements = driver.FindElement(By.XPath("//h5[contains(text(),'Elements')]"));   
            elements.Click();

            IWebElement textBoxElement = driver.FindElement(By.XPath("//ul/li/span[text()='Text Box']"));
            textBoxElement.Click();

            // Tìm kiếm element theo ID
            IWebElement userName = driver.FindElement(By.Id("userName")); 
            IWebElement userEmail = driver.FindElement(By.Id("userEmail"));
            IWebElement currentAddress = driver.FindElement(By.Id("currentAddress"));
            IWebElement permanentAddress = driver.FindElement(By.Id("permanentAddress"));

            // Input text
            userName.SendKeys("Nam");  
            userEmail.SendKeys("nhnam@gmail.com");
            currentAddress.SendKeys("Hà Nội");
            permanentAddress.SendKeys("Hà Nội");

            // Submit
            IWebElement btn_submit = driver.FindElement(By.Id("submit"));
            btn_submit.Click();

            // Check Output
            IWebElement output = driver.FindElement(By.Id("output"));

            TestContext.Out.WriteLine(output.Text);
            TestContext.Out.WriteLine(userName.Location);
            TestContext.Out.WriteLine(userEmail.Location);
            TestContext.Out.WriteLine(currentAddress.Location);
            TestContext.Out.WriteLine(permanentAddress.Location);
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
