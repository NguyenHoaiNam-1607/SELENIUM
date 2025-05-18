using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Selenium;

public class DriverTest
{
    IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal; // Default. Đợi trang tải hoàn toàn (HTML, CSS, JS, ảnh, font, v.v...). Độ chính xác cao
        //chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager; // Đợi khi DOM content loaded (HTML và script đã xử lý), không cần đợi ảnh/font. Trang nhẹ
        //chromeOptions.PageLoadStrategy = PageLoadStrategy.None;  // Không chờ gì cả, Selenium tiếp tục chạy ngay sau khi gửi lệnh điều hướng. Tối ưu hiêu năng
        // ** Với Eager và None nên dùng WebDriverWait để tránh lỗi "Element not found"
        driver = new ChromeDriver(chromeOptions);
        driver.Manage().Window.Maximize();  // Mở trình duyệt ở chế độ toàn màn hình                    
    }

    [Test]
    public void Test1()
    {
        // Mở trang web
        driver.Navigate().GoToUrl("https://demoqa.com");

        // Thông tin trang web
        string title = driver.Title;
        string pageURL = driver.Url;
        string pageSource = driver.PageSource;
        int pageSourceLength = pageSource.Length;

        // Output
        TestContext.Out.WriteLine($"Title: {title}");
        TestContext.Out.WriteLine($"Page URL: {pageURL}");
        TestContext.Out.WriteLine($"Page Source Length: {pageSourceLength}");

    }

    [TearDown]
    public void Teardown()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        if (driver != null)
        {
            driver.Quit();      // Đóng trình duyệt
            driver.Dispose();   // Giải phóng tài nguyên
        }
    }
}
