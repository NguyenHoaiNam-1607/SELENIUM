# [SELENIUM](https://www.selenium.dev/documentation/)

## Giới thiệu
### Selenium là gì
* **Selenium** là bộ công cụ mã nguồn mở được sử dụng để tự động hóa việc kiểm thử trên các ứng dụng web.
* Hỗ trợ nhiều trình duyệt: Chrome, Firefox, Edge, Safari.
* Hỗ trợ nhiều ngôn ngữ: Java, Python, C#, JavaScript.
### Các thành phần chính
* **Selenium IDE**: Ghi lại và phát lại hành động trên trình duyệt. Hỗ trợ Chrome/Firefox qua extension. 
* **Selenium WebDriver**: Tương tác trực tiếp với trình duyệt qua API. Hỗ trợ nhiều ngôn ngữ lập trình
* **Selenium Grid**: Chạy test song song trên nhiều máy và trình duyệt. Tối ưu cho testing phân tán.

## Selenium WebDriver
### Cài đặt cho C#(.NET)
* Packet Manager
	```bash
	Install-Package Selenium.WebDriver
	```
* .NET CLI
	```bash
	dotnet add package Selenium.WebDriver
	```
### Example
```csharp
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumDocs.GettingStarted;

public static class FirstScript
{
	public static void Main()
	{
		IWebDriver driver = new ChromeDriver();

		driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");

		var title = driver.Title;

		driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

		var textBox = driver.FindElement(By.Name("my-text"));
		var submitButton = driver.FindElement(By.TagName("button"));
			
		textBox.SendKeys("Selenium");
		submitButton.Click();
			
		var message = driver.FindElement(By.Id("message"));
		var value = message.Text;
			
		driver.Quit();
	}
}
```



## Locators
Trong Selenium, **Locator** là cách để xác định và tìm các phần tử HTML trên trang web. Việc xác định đúng locator là bước quan trọng để có thể tương tác tự động với trang web.

### Các loại Locator phổ biến

| Loại Locator         | Mô tả                                                | Ví dụ (C#)                               						|
|----------------------|------------------------------------------------------|-----------------------------------------------------------------|
| `By.Id`              | Tìm theo thuộc tính `id`                             | `driver.FindElement(By.Id("search"))`    						|
| `By.Name`            | Tìm theo thuộc tính `name`                           | `driver.FindElement(By.Name("q"))`       						|
| `By.ClassName`       | Tìm theo class CSS                                   | `driver.FindElement(By.ClassName("btn"))` 						|
| `By.TagName`         | Tìm theo tên thẻ HTML                                | `driver.FindElement(By.TagName("input"))` 						|
| `By.LinkText`        | Tìm liên kết theo nội dung chính xác của thẻ `<a>`   | `driver.FindElement(By.LinkText("Home"))` 						|
| `By.PartialLinkText` | Tìm liên kết theo một phần nội dung                  | `driver.FindElement(By.PartialLinkText("Hom"))` 				|
| `By.CssSelector`     | Dùng cú pháp CSS để tìm phần tử                      | `driver.FindElement(By.CssSelector("div > input[type='text']"))`|
| `By.XPath`           | Dùng cú pháp XPath để tìm phần tử (rất mạnh mẽ)      | `driver.FindElement(By.XPath("//input[@id='search']"))` 		|


### Xpath
**XPath (XML Path Language)** là ngôn ngữ dùng để xác định vị trí các phần tử trong tài liệu XML/HTML. Trong Selenium, XPath là một trong những phương pháp định vị phần tử mạnh mẽ nhất.

#### ✅ Ưu điểm của XPath

- Có thể truy cập phần tử ở bất kỳ đâu trong cây DOM.
- Cho phép duyệt qua các phần tử cha/con/anh em.
- Hỗ trợ tìm theo thuộc tính, nội dung văn bản, vị trí...

#### Cú pháp cơ bản của XPath

| Cú pháp                     			| Mô tả                                                     |
|---------------------------------------|-----------------------------------------------------------|
| `//tagname`                  			| Chọn tất cả phần tử theo tên thẻ                          |
| `//tagname[@attribute='value']` 		| Chọn phần tử có thuộc tính cụ thể                         |
| `//*[@id='search']`          			| Chọn bất kỳ phần tử nào có id là "search"                 |
| `//div[@class='header']//a`  			| Tìm thẻ `<a>` bên trong một `<div>` có class "header"     |
| `//input[@type='text'][1]`   			| Chọn phần tử đầu tiên phù hợp với điều kiện               |
| `//label[text()='Username']` 			| Chọn phần tử có nội dung văn bản đúng bằng 'Username'     |
| `//button[contains(text(),'Login')]` 	| Phần tử chứa một phần văn bản                             |
| `//div[contains(@class, 'active')]` 	| Phần tử có class chứa chữ "active"						|


Toán tử XPath thường dùng

| Toán tử       | Mô tả                           		| Ví dụ                                       |
|---------------|---------------------------------------|---------------------------------------------|
| `/`           | Chọn trực tiếp (từ cấp cha tới con) 	| `/html/body/div`                            |
| `//`          | Chọn từ bất kỳ vị trí trong cây DOM 	| `//input`                                   |
| `@`           | Chọn thuộc tính                   	| `//div[@id='main']`                         |
| `*`           | Đại diện cho bất kỳ thẻ nào       	| `//*[@class='btn']`                         |
| `contains()`  | So sánh chứa chuỗi con            	| `//a[contains(text(),'Next')]`              |
| `text()`      | So sánh nội dung văn bản          	| `//span[text()='Đăng nhập']`                |
| `and`, `or`   | Điều kiện kết hợp                 	| `//input[@type='text' and @name='email']`   |