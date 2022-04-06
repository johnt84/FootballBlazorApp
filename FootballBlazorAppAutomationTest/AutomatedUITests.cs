using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;

namespace FootballBlazorAppAutomationTest
{
    [TestClass]
    public class AutomatedUITests
    {
        IConfigurationRoot config = null;
        
        const string GROUPS_OR_LEAGUE_TABLE_PAGE = "groupsOrLeagueTable";
        const string FIXTURES_AND_RESULTS_PAGE = "fixturesandresults";
        const string FIXTURES_AND_RESULTS_HEADING = "Fixtures & Results";
        const string TEAMS_PAGE = "teams";
        const string TEAMS_HEADING = "Teams";

        ChromeDriver driver = null;

        [TestMethod]
        public void NavigateToGroupsOrLeagueTablePage()
        {
            Initialise();

            driver.Navigate().GoToUrl(config["FootballBlazorAppUrl"] + GROUPS_OR_LEAGUE_TABLE_PAGE);

            var firstH1Heading = driver.FindElement(By.XPath("//h1"));

            Assert.AreEqual(config["GroupsOrLeagueTableHeading"], firstH1Heading.Text);

            int numberOfRowsInTable = driver.FindElements(By.XPath("//tr")).Count;

            Assert.AreEqual(Convert.ToInt32(config["NumberOfTeamsInLeague"]) + 1, numberOfRowsInTable);

            Thread.Sleep(4000);

            driver.Quit();
        }

        [TestMethod]
        public void NavigateToFixturesAndResultsPage()
        {
            Initialise();

            driver.Navigate().GoToUrl(config["FootballBlazorAppUrl"] + FIXTURES_AND_RESULTS_PAGE);

            var firstH1Heading = driver.FindElement(By.XPath("//h1"));

            Assert.AreEqual(FIXTURES_AND_RESULTS_HEADING, firstH1Heading.Text);

            int numberOfRowsInTable = driver.FindElements(By.XPath("//tr")).Count;

            Assert.IsTrue(numberOfRowsInTable > 0);

            Thread.Sleep(4000);

            driver.Quit();
        }

        [TestMethod]
        public void NavigateToTeamsPage()
        {
            Initialise();

            driver.Navigate().GoToUrl(config["FootballBlazorAppUrl"] + TEAMS_PAGE);

            var firstH1Heading = driver.FindElement(By.XPath("//h1"));

            Assert.AreEqual(TEAMS_HEADING, firstH1Heading.Text);

            int numberOfItemsInList = driver.FindElements(By.XPath("//li")).Count;

            Assert.IsTrue(numberOfItemsInList > 0);

            Thread.Sleep(4000);

            driver.Quit();
        }

        [TestMethod]
        public void NavigateToTeamPage()
        {
            Initialise();

            driver.Navigate().GoToUrl(config["FootballBlazorAppUrl"] + TEAMS_PAGE);

            Thread.Sleep(2000);

            var firstTeamHyperlink = driver.FindElement(By.CssSelector("[href*='team/']"));

            string firstTeamUrl = firstTeamHyperlink.GetAttribute("href");
            string firstTeamName = firstTeamHyperlink.Text;

            firstTeamHyperlink.Click();

            Assert.AreEqual(firstTeamUrl, driver.Url);

            Thread.Sleep(2000);

            var firstH1TeamHeading = driver.FindElement(By.XPath("//h1"));

            Assert.AreEqual(firstTeamName, firstH1TeamHeading.Text);

            Thread.Sleep(4000);

            driver.Quit();
        }

        [TestMethod]
        public void NavigateToPlayerPage()
        {
            Initialise();

            driver.Navigate().GoToUrl(config["FootballBlazorAppUrl"] + TEAMS_PAGE);

            var firstTeamHyperlink = driver.FindElement(By.CssSelector("[href*='team/']"));

            firstTeamHyperlink.Click();

            var firstPlayerHyperlink = driver.FindElement(By.CssSelector("[href*='playerprofile/']"));

            string firstPlayerUrl = firstPlayerHyperlink.GetAttribute("href");
            string firstPlayerName = firstPlayerHyperlink.Text;

            firstPlayerHyperlink.Click();

            Assert.AreEqual(firstPlayerUrl, driver.Url);

            var firstH1PlayerHeading = driver.FindElement(By.XPath("//h1"));

            Assert.AreEqual(firstPlayerName, firstH1PlayerHeading.Text);

            driver.Quit();
        }

        private void Initialise()
        {
            config = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/../../..")
                .AddJsonFile("appsettings.json")
                .Build();

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
    }
}