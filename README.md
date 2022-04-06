# Football Blazor Server App

Simple Blazor Server Web App which displays information about a chosen competition.  Currently configured to the English Premier League.  Information shown consists of:

* Fixtures and Results
* Groups
* Teams and Players

Utilises the REST API called <a href="https://www.football-data.org/">Football Data API</a> which provides responses in JSON

* App is Developed using Blazor Server/.Net 6
* Data coming from the Football Data API is currently cached for a configurable number of hours (currently 3) into a class called FootballDataState which is an injected singleton class
* Contains 2 testing projects
  - A unit test app which uses MS Test .Net 6 and utilises the Moq 4.16 library to unit test the football data service
  - An automated UI testing app to test the Football Blazor Server App which utilises Selenium WebDriver
