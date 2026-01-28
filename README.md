# Football Blazor Server App

Simple Football Blazor Server Web App which displays information about a chosen competition.  Currently configured to the English Premier League.  Information shown consists of:

* Fixtures and Results
* Groups
* Teams and Players
* Player Search

The Player Search page was developed as a way to help with playing the Football based Wordle game called <a href="https://futboldle.com/">Futboldle</a>.  The player search tool helps with playing the Futboldle game as can search the list of competition players based on the following criteria:

* Player Name - wildcard search
* Team Name - wildcard search
* Player Age - between an age range
* Player Nationality - wildcard search
* Player Position - position the player plays in (Goalkeeper, Defence, Midfield, Attacker) - multi-select list
* Player Confederation (football confederation that player's nationality belongs to) - multi-select list
* Team Position - between a team position range in the league or group table

Utilises the REST API called <a href="https://www.football-data.org/">Football Data API</a> which provides responses in JSON

* App is Developed using Blazor Server/.Net 10
* Data coming from the Football Data API is currently cached for a configurable number of hours (currently 3) into a class called FootballDataState which is an injected singleton class
* Contains 2 testing projects
  - A unit test app which uses MS Test .Net 10 and utilises the Moq 4.20 library to unit test the football data service
  - An automated UI testing app to test the Football Blazor Server App which utilises Selenium WebDriver .Net 8
