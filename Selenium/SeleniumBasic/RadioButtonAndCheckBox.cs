using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium
{
    public class RadioButtonAndCheckBox
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
            driver.Url = "https://demoqa.com/automation-practice-form";

            IList<IWebElement> rdBtn_Gender = driver.FindElements(By.XPath("//input[@name='gender']//following-sibling::label"));
            bool rdValue = rdBtn_Gender[1].Selected;

            if (rdValue == true)
            {
                TestContext.Out.WriteLine("Checked: " + rdBtn_Gender[1].Text);
            }
            else
            {
                rdBtn_Gender[0].Click();
                TestContext.Out.WriteLine("Checked: " + rdBtn_Gender[0].Text);
            }

            IList<IWebElement> checkBox_Hobbies = driver.FindElements(By.XPath("//input[starts-with(@id, 'hobbies-checkbox')]/following-sibling::label"));
            foreach (var checkBox in checkBox_Hobbies)
            {
                if (checkBox.Selected)
                {
                    checkBox.Click();
                    TestContext.Out.WriteLine("Checked: " + checkBox.Text);
                }
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
