using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    public class ImplicitWaitAndExplicitWait
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //ImplicitWait - mặc định cho tất cả FindElement/FindElements. Web Driver sẽ chờ một khoảng thời gian trước khi throw exception
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://demoqa.com/text-box";

            IWebElement userName = driver.FindElement(By.XPath("//*[ @id=\"userName\"]"));
            IWebElement userEmail = driver.FindElement(By.XPath("//*[ @id=\"userEmail\"]"));
            IWebElement currentAddress = driver.FindElement(By.XPath("//*[ @id=\"currentAddress\"]"));
            IWebElement permanentAddress = driver.FindElement(By.XPath("//*[ @id=\"permanentAddress\"]"));

            userName.SendKeys("Nam");
            userEmail.SendKeys("nhnam@gmail.com");
            currentAddress.SendKeys("Ha Noi");
            permanentAddress.SendKeys("Ha Noi");

            //Explicit waits - Áp dụng trong từng trường hợp cụ thể, linh hoạt, dễ dàng tùy biến
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement btnSubmit = wait.Until(driver => driver.FindElement(By.XPath("//*[ @id=\"submit\"]")));
            btnSubmit.Click();

            IWebElement output = driver.FindElement(By.Id("output"));
            TestContext.Out.WriteLine(output.Text);
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
