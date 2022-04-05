using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace FootballBlazorAppAuotmationTest
{
    [TestClass]
    public class AutomatedUITests
    {
        //const string FOOTBALL_BLAZOR_APP_URL = "https://premierleagueblazorapp.azurewebsites.net/";
        const string FOOTBALL_BLAZOR_APP_URL = "https://localhost:5001/";
        const string GROUPS_OR_LEAGUE_TABLE_PAGE = "groupsOrLeagueTable";
        const int NUMBER_OF_TEAMS_IN_LEAGUE = 20;
        const string GROUPS_OR_LEAGUE_TABLE_HEADING = "Premier League Table";
        const string FIXTURES_AND_RESULTS_PAGE = "fixturesandresults";
        const string FIXTURES_AND_RESULTS_HEADING = "Fixtures & Results";

        ChromeDriver driver = null;

        [TestMethod]
        public void NavigateToGroupsOrLeagueTablePage()
        {
            Initialise();

            driver.Navigate().GoToUrl(FOOTBALL_BLAZOR_APP_URL + GROUPS_OR_LEAGUE_TABLE_PAGE);

            var firstH1Heading = driver.FindElement(By.XPath("//h1"));

            Assert.AreEqual(GROUPS_OR_LEAGUE_TABLE_HEADING, firstH1Heading.Text);

            int numberOfRowsInTable = driver.FindElements(By.XPath("//tr")).Count;

            Assert.AreEqual(NUMBER_OF_TEAMS_IN_LEAGUE + 1, numberOfRowsInTable);

            System.Threading.Thread.Sleep(2000);

            driver.Quit();
        }

        [TestMethod]
        public void NavigateToFixturesAndResultsPage()
        {
            Initialise();

            driver.Navigate().GoToUrl(FOOTBALL_BLAZOR_APP_URL + FIXTURES_AND_RESULTS_PAGE);

            var firstH1Heading = driver.FindElement(By.XPath("//h1"));

            Assert.AreEqual(FIXTURES_AND_RESULTS_HEADING, firstH1Heading.Text);

            int numberOfRowsInTable = driver.FindElements(By.XPath("//tr")).Count;

            Assert.IsTrue(numberOfRowsInTable > 0);

            System.Threading.Thread.Sleep(2000);

            driver.Quit();
        }

        private void Initialise()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
    }
}