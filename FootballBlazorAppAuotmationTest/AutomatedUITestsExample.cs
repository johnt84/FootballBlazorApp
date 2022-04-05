using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FootballBlazorAppAuotmationTest
{
    [TestClass]
    public class AutomatedUITestsExample
    {
        string test_url = "https://lambdatest.github.io/sample-todo-app/";
        string itemName = "Yey, Let's add it to list";

        [TestMethod]
        public void NavigateToDoApp()
        {
            IWebDriver driver;

            // Local Selenium WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test_url);

            driver.Manage().Window.Maximize();

            // Click on First Check box
            IWebElement firstCheckBox = driver.FindElement(By.Name("li1"));
            firstCheckBox.Click();

            // Click on Second Check box
            IWebElement secondCheckBox = driver.FindElement(By.Name("li2"));
            secondCheckBox.Click();

            // Enter Item name
            IWebElement textfield = driver.FindElement(By.Id("sampletodotext"));
            textfield.SendKeys(itemName);

            // Click on Add button
            IWebElement addButton = driver.FindElement(By.Id("addbutton"));
            addButton.Click();

            // Verified Added Item name
            IWebElement itemtext = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span"));
            string getText = itemtext.Text;
            Assert.IsTrue(itemName.Contains(getText));

            /* Perform wait to check the output in this MSTest tutorial for Selenium */
            System.Threading.Thread.Sleep(4000);

            driver.Quit();
        }
    }
}