using FootballEngine.API.Interfaces;
using FootballEngine.Services;
using FootballShared.Models;
using FootballShared.Models.FootballData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FootballEngineUnitTest
{
    [TestClass]
    public class FootballDataServiceTest
    {
        private FootballEngineInput footballEngineInput;

        private FootballDataState footballDataState;

        private Mock<IHttpAPIClient> mockHttpAPIClient;

        private string footballDataMatchesJson = string.Empty;

        private string footballDataStandingsJson = string.Empty;

        private string footballDataTeamsJson = string.Empty;

        private int teamId = 0;

        private void SetupTests()
        {
            footballEngineInput = new FootballEngineInput()
            {
                FootballDataAPIUrl = "https://api.testfootballdata.com/",
                Competition = "PL",
                HasGroups = false,
                LeagueName = "Premier League",
                Title = "PremierLeagueBlazorApp",
                APIToken = "",
                HoursUntilRefreshCache = 3,
            };

            footballDataTeamsJson = @"{
   ""count"":20,
   ""filters"":{
      
   },
   ""competition"":{
      ""id"":2021,
      ""area"":{
         ""id"":2072,
         ""name"":""England""
      },
      ""name"":""Premier League"",
      ""code"":""PL"",
      ""plan"":""TIER_ONE"",
      ""lastUpdated"":""2021-04-17T02:20:14Z""
   },
   ""season"":{
      ""id"":733,
      ""startDate"":""2021-08-13"",
      ""endDate"":""2022-05-22"",
      ""currentMatchday"":27,
      ""winner"":null
   },
   ""teams"":[
      {
         ""id"":57,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Arsenal FC"",
         ""shortName"":""Arsenal"",
         ""tla"":""ARS"",
         ""crest"":""https://crests.football-data.org/57.png"",
         ""address"":""75 Drayton Park London N5 1BU"",
         ""phone"":""+44 (020) 76195003"",
         ""website"":""http://www.arsenal.com"",
         ""email"":""info@arsenal.co.uk"",
         ""founded"":1886,
         ""clubColors"":""Red / White"",
         ""venue"":""Emirates Stadium"",
         ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
            }
         ],
         ""lastUpdated"":""2022-02-10T19:48:56Z""
      },
      {
         ""id"":58,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Aston Villa FC"",
         ""shortName"":""Aston Villa"",
         ""tla"":""AST"",
         ""crest"":""https://crests.football-data.org/58.svg"",
         ""address"":""Villa Park Birmingham B6 6HE"",
         ""phone"":""+44 (0121) 3272299"",
         ""website"":""http://www.avfc.co.uk"",
         ""email"":null,
         ""founded"":1872,
         ""clubColors"":""Claret / Sky Blue"",
         ""venue"":""Villa Park"",
         ""coach"":{
                    ""id"":71303,
                    ""firstName"":""Steven"",
                    ""lastName"":""Gerrard"",
                    ""name"":""Steven Gerrard"",
                    ""dateOfBirth"":""1980-05-30"",
                    ""nationality"":""England"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Lucas Digne"",
               ""position"":""Defence"",
               ""dateOfBirth"":""1993-07-20"",
               ""nationality"":""France""
           }
        ],
         ""lastUpdated"":""2021-03-03T09:46:16Z""
      },
      {
         ""id"":61,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Chelsea FC"",
         ""shortName"":""Chelsea"",
         ""tla"":""CHE"",
         ""crest"":""https://crests.football-data.org/61.png"",
         ""address"":""Fulham Road London SW6 1HS"",
         ""phone"":""+44 (0871) 9841955"",
         ""website"":""http://www.chelseafc.com"",
         ""email"":null,
         ""founded"":1905,
         ""clubColors"":""Royal Blue / White"",
         ""venue"":""Stamford Bridge"",
         ""coach"":{
            ""id"":72766,
            ""firstName"":""Thomas"",
            ""lastName"":null,
            ""name"":""Thomas Tuchel"",
            ""dateOfBirth"":""1973-08-29"",
            ""nationality"":""Germany"",
            ""contract"":{
               ""start"":null,
               ""until"":null
            }
         },
        ""squad"":[
            {
               ""id"":805,
               ""name"":""Edouard Mendy"",
               ""position"":""Goalkeeper"",
               ""dateOfBirth"":""1992-03-01"",
               ""nationality"":""Senegal""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:24:40Z""
      },
      {
         ""id"":62,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Everton FC"",
         ""shortName"":""Everton"",
         ""tla"":""EVE"",
         ""crest"":""https://crests.football-data.org/62.png"",
         ""address"":""Goodison Park Liverpool L4 4EL"",
         ""phone"":""+44 (0871) 6631878"",
         ""website"":""http://www.evertonfc.com"",
         ""email"":""everton@evertonfc.com"",
         ""founded"":1878,
         ""clubColors"":""Blue / White"",
         ""venue"":""Goodison Park"",
         ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Frank"",
                    ""lastName"":""Lampard"",
                    ""name"":""Frank Lampard"",
                    ""dateOfBirth"":""1978-06-20"",
                    ""nationality"":""england"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":138174,
               ""name"":""Nathan Patterson"",
               ""position"":""Defence"",
               ""dateOfBirth"":""2001-10-16"",
               ""nationality"":""Scotland""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:47:42Z""
      },
      {
         ""id"":64,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Liverpool FC"",
         ""shortName"":""Liverpool"",
         ""tla"":""LIV"",
         ""crest"":""https://crests.football-data.org/64.png"",
         ""address"":""Anfield Road Liverpool L4 0TH"",
         ""phone"":""+44 (0844) 4993000"",
         ""website"":""http://www.liverpoolfc.tv"",
         ""email"":""customercontact@liverpoolfc.tv"",
         ""founded"":1892,
         ""clubColors"":""Red / White"",
         ""venue"":""Anfield"",
         ""coach"":{
                    ""id"":11619,
                    ""firstName"":""J�rgen"",
                    ""lastName"":""Klopp"",
                    ""name"":""J�rgen Klopp"",
                    ""dateOfBirth"":""1967-06-16"",
                    ""nationality"":""Germany"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":7868,
               ""name"":""Andrew Robertson"",
               ""position"":""Defence"",
               ""dateOfBirth"":""1994-03-11"",
               ""nationality"":""Scotland""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:30:22Z""
      },
      {
         ""id"":65,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Manchester City FC"",
         ""shortName"":""Man City"",
         ""tla"":""MCI"",
         ""crest"":""https://crests.football-data.org/65.png"",
         ""address"":""SportCity Manchester M11 3FF"",
         ""phone"":""+44 (0870) 0621894"",
         ""website"":""https://www.mancity.com"",
         ""email"":""mancity@mancity.com"",
         ""founded"":1880,
         ""clubColors"":""Sky Blue / White"",
         ""venue"":""Etihad Stadium"",
        ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Pep"",
                    ""lastName"":null,
                    ""name"":""Pep Guardiola"",
                    ""dateOfBirth"":""1971-01-18"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
             {
               ""id"":3654,
               ""name"":""Kevin De Bruyne"",
               ""position"":""Midfield"",
               ""dateOfBirth"":""1991-06-28"",
               ""nationality"":""Belgium""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:48:37Z""
      },
      {
         ""id"":66,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Manchester United FC"",
         ""shortName"":""Man United"",
         ""tla"":""MUN"",
         ""crest"":""https://crests.football-data.org/66.png"",
         ""address"":""Sir Matt Busby Way Manchester M16 0RA"",
         ""phone"":""+44 (0161) 8688000"",
         ""website"":""http://www.manutd.com"",
         ""email"":""enquiries@manutd.co.uk"",
         ""founded"":1878,
         ""clubColors"":""Red / White"",
         ""venue"":""Old Trafford"",
        ""coach"":{
                    ""id"":9584,
                    ""firstName"":""Erik"",
                    ""lastName"":null,
                    ""name"":""Erik ten Hag"",
                    ""dateOfBirth"":""1970-02-02"",
                    ""nationality"":""Netherlands"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":3459,
               ""name"":""Christian Eriksen"",
               ""position"":""Midfield"",
               ""dateOfBirth"":""1992-02-14"",
               ""nationality"":""Denmark""
           }
        ],
         ""lastUpdated"":""2022-02-10T19:27:46Z""
      },
      {
         ""id"":67,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Newcastle United FC"",
         ""shortName"":""Newcastle"",
         ""tla"":""NEW"",
         ""crest"":""https://crests.football-data.org/67.png"",
         ""address"":""Sports Direct Arena Newcastle upon Tyne NE1 4ST"",
         ""phone"":null,
         ""website"":""http://www.nufc.co.uk"",
         ""email"":""admin@nufc.co.uk"",
         ""founded"":1881,
         ""clubColors"":""Black / White"",
         ""venue"":""St. James' Park"",
         ""coach"":{
            ""id"":11594,
            ""firstName"":""Eddie"",
            ""lastName"":null,
            ""name"":""Eddie Howe"",
            ""dateOfBirth"":""1977-11-29"",
            ""nationality"":""England"",
            ""contract"":{
               ""start"":null,
               ""until"":null
            }
         },
         ""squad"":[
           {
               ""id"":8446,
               ""name"":""Allan Saint-Maximin"",
               ""position"":""Offence"",
               ""dateOfBirth"":""1997-03-12"",
               ""nationality"":""France""
           }
        ],
         ""lastUpdated"":""2022-02-10T19:22:56Z""
      },
      {
         ""id"":68,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Norwich City FC"",
         ""shortName"":""Norwich"",
         ""tla"":""NOR"",
         ""crest"":""https://upload.wikimedia.org/wikipedia/en/8/8c/Norwich_City.svg"",
         ""address"":""Carrow Road Norwich NR1 1JE"",
         ""phone"":null,
         ""website"":""http://www.canaries.co.uk"",
         ""email"":""reception@ncfc-canaries.co.uk"",
         ""founded"":1902,
         ""clubColors"":""Yellow / Green"",
         ""venue"":""Carrow Road"",
         ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
            }
        ],
         ""lastUpdated"":""2021-04-20T18:23:20Z""
      },
      {
         ""id"":73,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Tottenham Hotspur FC"",
         ""shortName"":""Tottenham"",
         ""tla"":""TOT"",
         ""crest"":""https://crests.football-data.org/73.svg"",
         ""address"":""Bill Nicholson Way, 748 High Road London N17 0AP"",
         ""phone"":""+44 (0844) 4995000"",
         ""website"":""http://www.tottenhamhotspur.com"",
         ""email"":""customer.care@tottenhamhotspur.com"",
         ""founded"":1882,
         ""clubColors"":""Navy Blue / White"",
         ""venue"":""Tottenham Hotspur Stadium"",
        ""coach"":{
            ""id"":11583,
            ""firstName"":""Antonio"",
            ""lastName"":""Conte"",
            ""name"":""Antonio Conte"",
            ""dateOfBirth"":""1969-07-31"",
            ""nationality"":""Italy"",
            ""contract"":{
               ""start"":null,
               ""until"":null
            }
         },
         ""squad"":[
                 {
                       ""id"":8004,
                       ""name"":""Harry Kane"",
                       ""position"":""Offence"",
                       ""dateOfBirth"":""1993-07-28"",
                       ""nationality"":""England""
                    }
        ],
         ""lastUpdated"":""2020-11-20T07:12:32Z""
      },
      {
         ""id"":76,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Wolverhampton Wanderers FC"",
         ""shortName"":""Wolverhampton"",
         ""tla"":""WOL"",
         ""crest"":""https://crests.football-data.org/76.svg"",
         ""address"":""Waterloo Road Wolverhampton WV1 4QR"",
         ""phone"":""+44 (0871) 2222220"",
         ""website"":""http://www.wolves.co.uk"",
         ""email"":""info@wolves.co.uk"",
         ""founded"":1877,
         ""clubColors"":""Black / Gold"",
         ""venue"":""Molineux Stadium"",
        ""coach"":{
            ""id"":80460,
            ""firstName"":""Bruno Miguel Silva do"",
            ""lastName"":null,
            ""name"":""Bruno Lage"",
            ""dateOfBirth"":""1976-05-12"",
            ""nationality"":""Portugal"",
            ""contract"":{
               ""start"":null,
               ""until"":null
            }
         },
         ""squad"":[
            {
               ""id"":3249,
               ""name"":""Jo�o Moutinho"",
               ""position"":""Midfield"",
               ""dateOfBirth"":""1986-09-08"",
               ""nationality"":""Portugal""
            }
        ],
         ""lastUpdated"":""2021-04-09T02:25:24Z""
      },
      {
         ""id"":328,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Burnley FC"",
         ""shortName"":""Burnley"",
         ""tla"":""BUR"",
         ""crest"":""https://crests.football-data.org/328.png"",
         ""address"":""Harry Potts Way Burnley BB10 4BX"",
         ""phone"":""+44 (0871) 2211882"",
         ""website"":""http://www.burnleyfootballclub.com"",
         ""email"":""info@burnleyfc.com"",
         ""founded"":1881,
         ""clubColors"":""Claret / Sky Blue"",
         ""venue"":""Turf Moor"",
        ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:24:11Z""
      },
      {
         ""id"":338,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Leicester City FC"",
         ""shortName"":""Leicester City"",
         ""tla"":""LEI"",
         ""crest"":""https://crests.football-data.org/338.png"",
         ""address"":""The Walkers Stadium, Filbert Way Leicester LE2 7FL"",
         ""phone"":""+44 (0844) 8156000"",
         ""website"":""http://www.lcfc.com"",
         ""email"":null,
         ""founded"":1884,
         ""clubColors"":""Royal Blue / White"",
         ""venue"":""King Power Stadium"",
       ""coach"":{
            ""id"":15624,
            ""firstName"":""Brendan"",
            ""lastName"":null,
            ""name"":""Brendan Rodgers"",
            ""dateOfBirth"":""1973-02-19"",
            ""nationality"":""Northern Ireland"",
            ""contract"":{
               ""start"":null,
               ""until"":null
            }
         },
         ""squad"":[
           {
               ""id"":3992,
               ""name"":""James Maddison"",
               ""position"":""Midfield"",
               ""dateOfBirth"":""1996-11-23"",
               ""nationality"":""England""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:48:23Z""
      },
      {
         ""id"":340,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Southampton FC"",
         ""shortName"":""Southampton"",
         ""tla"":""SOU"",
         ""crest"":""https://crests.football-data.org/340.png"",
         ""address"":""Britannia Road Southampton SO14 5FP"",
         ""phone"":null,
         ""website"":""http://www.saintsfc.co.uk"",
         ""email"":""sfc@saintsfc.co.uk"",
         ""founded"":1885,
         ""clubColors"":""Red / White / Black"",
         ""venue"":""St. Mary's Stadium"",
        ""coach"":{
            ""id"":43924,
            ""firstName"":""Ralph"",
            ""lastName"":null,
            ""name"":""Ralph Hasenh�ttl"",
            ""dateOfBirth"":""1967-08-09"",
            ""nationality"":""Austria"",
            ""contract"":{
               ""start"":null,
               ""until"":null
            }
         },
         ""squad"":[
            {
               ""id"":6318,
               ""name"":""Joe Aribo"",
               ""position"":""Midfield"",
               ""dateOfBirth"":""1996-07-21"",
               ""nationality"":""Nigeria""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:48:04Z""
      },
      {
         ""id"":341,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Leeds United FC"",
         ""shortName"":""Leeds United"",
         ""tla"":""LEE"",
         ""crest"":""https://crests.football-data.org/341.png"",
         ""address"":""Elland Road Leeds LS11 0ES"",
         ""phone"":""+44 (0871) 3341919"",
         ""website"":""http://www.leedsunited.com"",
         ""email"":""reception@leedsunited.com"",
         ""founded"":1904,
         ""clubColors"":""White / Blue"",
         ""venue"":""Elland Road"",
       ""coach"":{
            ""id"":59069,
            ""firstName"":""Jesse"",
            ""lastName"":null,
            ""name"":""Jesse Marsch"",
            ""dateOfBirth"":""1973-11-08"",
            ""nationality"":""United States"",
            ""contract"":{
               ""start"":null,
               ""until"":null
            }
         },
         ""squad"":[
            {
               ""id"":4135,
               ""name"":""Liam Cooper"",
               ""position"":""Defence"",
               ""dateOfBirth"":""1991-08-30"",
               ""nationality"":""Scotland""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:27:14Z""
      },
      {
         ""id"":346,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Watford FC"",
         ""shortName"":""Watford"",
         ""tla"":""WAT"",
         ""crest"":""https://crests.football-data.org/346.svg"",
         ""address"":""Vicarage Road Watford WD18 0ER"",
         ""phone"":null,
         ""website"":""http://www.watfordfc.com"",
         ""email"":""yourvoice@watfordfc.com"",
         ""founded"":1881,
         ""clubColors"":""Yellow / Black"",
         ""venue"":""Vicarage Road Stadium"",
        ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
           }
        ],
         ""lastUpdated"":""2020-11-26T02:16:48Z""
      },
      {
         ""id"":354,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Crystal Palace FC"",
         ""shortName"":""Crystal Palace"",
         ""tla"":""CRY"",
         ""crest"":""https://crests.football-data.org/354.png"",
         ""address"":""Whitehorse Lane London SE25 6PU"",
         ""phone"":""+44 (020) 87686000"",
         ""website"":""http://www.cpfc.co.uk"",
         ""email"":""info@cpfc.co.uk"",
         ""founded"":1905,
         ""clubColors"":""Red / Blue"",
         ""venue"":""Selhurst Park"",
        ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:22:36Z""
      },
      {
         ""id"":397,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Brighton & Hove Albion FC"",
         ""shortName"":""Brighton Hove"",
         ""tla"":""BHA"",
         ""crest"":""https://crests.football-data.org/397.svg"",
         ""address"":""44 North Road Brighton & Hove BN1 1YR"",
         ""phone"":""+44 (01273) 878288"",
         ""website"":""http://www.seagulls.co.uk"",
         ""email"":""seagulls@bhafc.co.uk"",
         ""founded"":1898,
         ""clubColors"":""Blue / White"",
         ""venue"":""The American Express Community Stadium"",
        ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
            }
        ],
         ""lastUpdated"":""2021-04-12T13:10:44Z""
      },
      {
         ""id"":402,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""Brentford FC"",
         ""shortName"":""Brentford"",
         ""tla"":""BRF"",
         ""crest"":""https://crests.football-data.org/402.png"",
         ""address"":""Braemar Road Brentford TW8 0NT"",
         ""phone"":null,
         ""website"":""http://www.brentfordfc.co.uk"",
         ""email"":""enquiries@brentfordfc.co.uk"",
         ""founded"":1889,
         ""clubColors"":""Red / White / Black"",
         ""venue"":""Griffin Park"",
        ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
            }
        ],
         ""lastUpdated"":""2022-02-10T19:22:16Z""
      },
      {
         ""id"":563,
         ""area"":{
            ""id"":2072,
            ""name"":""England""
         },
         ""name"":""West Ham United FC"",
         ""shortName"":""West Ham"",
         ""tla"":""WHU"",
         ""crest"":""https://crests.football-data.org/563.png"",
         ""address"":""Queen Elizabeth Olympic Park, London London E20 2ST"",
         ""phone"":""+44 (020) 85482794"",
         ""website"":""http://www.whufc.com"",
         ""email"":""yourcomments@westhamunited.co.uk"",
         ""founded"":1895,
         ""clubColors"":""Claret / Sky Blue"",
         ""venue"":""London Stadium"",
        ""coach"":{
                    ""id"":11619,
                    ""firstName"":""Mikel"",
                    ""lastName"":null,
                    ""name"":""Arteta"",
                    ""dateOfBirth"":""1982-03-26"",
                    ""nationality"":""Spain"",
                    ""contract"":{
                       ""start"":null,
                       ""until"":null
                    }
                 },
         ""squad"":[
            {
               ""id"":99813,
               ""name"":""Bukayo Saka"",
               ""position"":""Offence"",
               ""dateOfBirth"":""2001-09-05"",
               ""nationality"":""England""
            }
        ],
         ""lastUpdated"":""2022-02-19T08:09:25Z""
      }
   ]
}";

            footballDataMatchesJson = @"
{
   ""count"":380,
   ""filters"":{
      
   },
   ""competition"":{
      ""id"":2021,
      ""area"":{
         ""id"":2072,
         ""name"":""England""
      },
      ""name"":""Premier League"",
      ""code"":""PL"",
      ""plan"":""TIER_ONE"",
      ""lastUpdated"":""2021-04-17T02:20:14Z""
   },
   ""matches"":[
      {
         ""id"":327362,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-13T19:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""shortName"":""Brentford""
         },
         ""awayTeam"":{
            ""id"":57,
            ""shortName"":""Arsenal""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327357,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-14T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":5,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""shortName"":""Manchester United""
         },
         ""awayTeam"":{
            ""id"":341,
            ""shortName"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327353,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-14T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""shortName"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""shortName"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327358,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-14T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""shortName"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""shortName"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327359,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-14T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""shortName"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""shortName"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327360,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-14T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""shortName"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""shortName"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11324,
               ""name"":""James Linington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327361,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-14T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""shortName"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""shortName"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327355,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-14T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""shortName"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""shortName"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327356,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-15T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""shortName"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""shortName"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327354,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-15T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":1,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""shortName"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""shortName"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327347,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-21T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""shortName"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""shortName"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327346,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-21T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":5,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""shortName"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""shortName"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327348,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-21T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""shortName"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""shortName"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327349,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-21T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""shortName"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""shortName"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327351,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-21T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""shortName"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""shortName"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11626,
               ""name"":""Matt Donohue"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327350,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-21T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""shortName"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""shortName"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327343,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-22T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""shortName"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""shortName"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327345,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-22T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""shortName"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""shortName"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327352,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-22T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""shortName"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""shortName"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327344,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-23T19:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":2,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""shortName"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""shortName"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327338,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-28T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":5,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""shortName"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""shortName"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327334,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""shortName"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""shortName"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":172508,
               ""name"":""Josh Smith"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327336,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""shortName"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""shortName"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11519,
               ""name"":""Keith Stroud"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327337,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""shortName"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""shortName"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327341,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""shortName"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""shortName"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327342,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""shortName"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""shortName"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11287,
               ""name"":""Oliver Langford"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327339,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-28T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""shortName"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""shortName"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327335,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-29T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""shortName"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""shortName"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327340,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-29T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""shortName"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""shortName"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327333,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-08-29T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":3,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""shortName"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327329,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""shortName"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""shortName"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327323,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""shortName"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""shortName"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11549,
               ""name"":""David Webb"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327324,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""shortName"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""shortName"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327325,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327326,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327331,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11486,
               ""name"":""Dean Whitestone"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327332,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":9382,
               ""name"":""Gavin Ward"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327330,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-11T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327327,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-12T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327328,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-13T19:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":4,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327317,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-17T19:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327313,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-18T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327316,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-18T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11396,
               ""name"":""Tim Robinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327318,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-18T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327319,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-18T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11312,
               ""name"":""Geoff Eltringham"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327320,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-18T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327322,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-18T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327314,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-19T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327321,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-19T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327315,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-19T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":5,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327305,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-25T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327310,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-25T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327303,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-25T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""REFEREE"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327306,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-25T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327307,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-25T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327308,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-25T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11486,
               ""name"":""Dean Whitestone"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327311,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-25T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327304,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-26T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327312,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-26T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327309,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-09-27T19:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":6,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327296,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-02T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327293,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-02T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327298,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-02T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327300,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-02T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327301,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-02T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327302,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-02T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327294,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-03T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327295,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-03T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327299,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-03T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327297,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-03T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":7,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327283,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-16T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":5
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327284,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-16T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327285,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-16T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11396,
               ""name"":""Tim Robinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327287,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-16T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327288,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-16T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327291,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-16T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327290,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-16T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327289,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-17T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327286,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-17T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":3
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327292,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-18T19:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":8,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327282,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-22T19:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327279,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-23T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":7,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327274,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-23T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327276,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-23T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327277,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-23T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":5
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327278,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-23T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":null
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327280,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-23T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327273,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-24T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327281,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-24T13:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327275,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-24T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":9,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":5
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":4
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327270,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-30T11:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327264,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-30T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11519,
               ""name"":""Keith Stroud"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327267,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-30T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327268,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-30T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327269,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-30T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327271,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-30T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11472,
               ""name"":""Jeremy Simpson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327265,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-30T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327266,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-31T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327272,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-10-31T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327263,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-01T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":10,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327254,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-05T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327255,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-06T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327258,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-06T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":9382,
               ""name"":""Gavin Ward"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327259,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-06T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327261,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-06T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""REFEREE"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327260,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-06T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":null
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11519,
               ""name"":""Keith Stroud"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327256,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-07T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327257,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-07T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327262,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-07T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327253,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-07T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":11,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327250,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327243,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":9382,
               ""name"":""Gavin Ward"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327244,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11486,
               ""name"":""Dean Whitestone"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327246,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11626,
               ""name"":""Matt Donohue"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327247,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11472,
               ""name"":""Jeremy Simpson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327251,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11549,
               ""name"":""David Webb"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327252,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327249,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-20T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327248,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-21T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327245,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-21T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":12,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327242,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-27T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327233,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-27T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327235,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-27T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327237,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-27T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327240,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-27T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327234,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327236,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327241,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-28T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327238,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-28T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327225,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-30T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327230,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-11-30T20:15:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327224,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-01T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327227,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-01T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327228,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-01T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327229,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-01T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327231,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-01T20:15:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327232,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-01T20:15:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327223,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-02T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327226,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-02T20:15:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":14,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327214,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-04T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327213,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-04T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327217,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-04T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327218,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-04T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327215,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-04T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327216,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-05T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""REFEREE"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327219,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-05T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327220,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-05T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":null
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327222,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-05T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327221,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-06T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":15,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327211,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-10T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327204,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-11T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327205,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-11T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327208,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-11T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327212,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-11T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""REFEREE"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327203,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-11T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327206,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-12T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327209,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-12T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327207,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-12T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327197,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-14T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327193,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-14T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":7,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327196,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-15T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327200,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-15T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327202,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-15T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327195,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-16T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327194,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-16T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327192,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-18T15:00:00Z"",
         ""status"":""POSTPONED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-12-18T13:53:03Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11626,
               ""name"":""Matt Donohue"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327190,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-18T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327183,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-19T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327188,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-19T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327186,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-19T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327174,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-26T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327175,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-26T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11290,
               ""name"":""Steve Martin"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327176,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-26T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":5
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11486,
               ""name"":""Dean Whitestone"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327178,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-26T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":6,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":4,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327180,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-26T15:00:00Z"",
         ""status"":""POSTPONED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-12-24T16:20:13Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11312,
               ""name"":""Geoff Eltringham"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":null
            }
         ]
      },
      {
         ""id"":327182,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-26T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327181,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-26T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11519,
               ""name"":""Keith Stroud"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327177,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-27T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327168,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-28T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327169,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-28T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327163,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-28T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11396,
               ""name"":""Tim Robinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327165,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-28T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327170,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-29T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327171,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-29T20:15:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327164,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2021-12-30T20:15:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327162,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-01T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327153,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-01T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327156,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-01T15:00:00Z"",
         ""status"":""POSTPONED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-12-30T22:36:01Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327159,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-01T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""Australia""
            }
         ]
      },
      {
         ""id"":327157,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-02T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327158,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-02T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327161,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-02T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327160,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-02T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327155,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-03T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327187,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-11T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":4,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327191,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-11T20:00:00Z"",
         ""status"":""POSTPONED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-10T00:20:13Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327184,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-12T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327151,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-14T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327148,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-15T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327143,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-15T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327146,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-15T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":172508,
               ""name"":""Josh Smith"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":null
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327147,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-15T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11486,
               ""name"":""Dean Whitestone"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":172508,
               ""name"":""Josh Smith"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":null
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327152,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-15T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327144,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-16T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327149,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-16T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327145,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-16T16:30:00Z"",
         ""status"":""POSTPONED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-16T00:20:16Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327131,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-18T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327198,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-19T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327201,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-19T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327133,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-21T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327138,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-22T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327135,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-22T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327137,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-22T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327141,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-22T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327134,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-22T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327136,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-23T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327139,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-23T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327142,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-23T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327140,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-01-23T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":23,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327199,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-05T18:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":17,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327126,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-08T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327128,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-08T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327130,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-08T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327123,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-09T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327125,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-09T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327129,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-09T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327132,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-09T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""REFEREE"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327124,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-10T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327127,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-10T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":24,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327117,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-12T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327113,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-12T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327119,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-12T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327120,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-12T15:00:00Z"",
         ""status"":""POSTPONED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-11-30T16:20:12Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327122,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-12T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327115,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-12T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327114,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-13T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327116,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-13T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327121,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-13T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327118,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-13T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":25,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327189,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-15T20:15:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327104,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327105,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":57567,
               ""name"":""Wade Smith "",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11519,
               ""name"":""Keith Stroud"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327107,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":3,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11626,
               ""name"":""Matt Donohue"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327109,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11405,
               ""name"":""Michael Salisbury"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327110,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11486,
               ""name"":""Dean Whitestone"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327111,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327112,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327106,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-19T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":3
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327108,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-20T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327103,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-20T16:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":26,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327185,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-23T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":18,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327239,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-23T19:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":13,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11431,
               ""name"":""Daniel Robathan"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11494,
               ""name"":""Stuart Attwell"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327179,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-23T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":6,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":3,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327172,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-24T19:45:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327094,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-25T20:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":2,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            {
               ""id"":11504,
               ""name"":""Simon Long"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":73363,
               ""name"":""Nick Hopton"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11430,
               ""name"":""Simon Hooper"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":98555,
               ""name"":""Sian Massey-Ellis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327096,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-26T12:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":4
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":3
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11495,
               ""name"":""Ian Hussin"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327095,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-26T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11544,
               ""name"":""Simon Beck"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11486,
               ""name"":""Dean Whitestone"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327098,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-26T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""DRAW"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            {
               ""id"":11567,
               ""name"":""Jonathan Moss"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11531,
               ""name"":""Marc Perry"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":172508,
               ""name"":""Josh Smith"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":null
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327101,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-26T15:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11575,
               ""name"":""Mike Dean"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11469,
               ""name"":""Darren England"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11505,
               ""name"":""Derek Eaton"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327100,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-26T15:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":2
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11327,
               ""name"":""John Brooks"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11444,
               ""name"":""Matthew Wilkes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":null
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11605,
               ""name"":""Michael Oliver"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11611,
               ""name"":""Scott Ledger"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327097,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-26T17:30:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-28T16:20:23Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""AWAY_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":0,
               ""awayTeam"":1
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            {
               ""id"":11520,
               ""name"":""Paul Tierney"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327093,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-27T14:00:00Z"",
         ""status"":""FINISHED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-03-01T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":""HOME_TEAM"",
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":1,
               ""awayTeam"":0
            },
            ""halfTime"":{
               ""homeTeam"":0,
               ""awayTeam"":0
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11585,
               ""name"":""Craig Pawson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11576,
               ""name"":""Darren Cann"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327099,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-02-27T14:00:00Z"",
         ""status"":""POSTPONED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-13T16:20:21Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327150,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-01T19:45:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":22,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-11T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            {
               ""id"":11564,
               ""name"":""Stuart Burt"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11378,
               ""name"":""Tony Harrington"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11530,
               ""name"":""Lee Betts"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11606,
               ""name"":""Constantine Hatzidakis"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11580,
               ""name"":""Anthony Taylor"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327090,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-05T12:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327083,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-05T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327086,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-05T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327087,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-05T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327091,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-05T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327092,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-05T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327089,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-05T17:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327084,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-06T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327088,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-06T16:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327085,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-07T20:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":28,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327173,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-10T19:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":19,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11424,
               ""name"":""Neil Davies"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":23568,
               ""name"":""Jarred Gillett"",
               ""role"":""REFEREE"",
               ""nationality"":""Australia""
            },
            {
               ""id"":11309,
               ""name"":""Peter Bankes"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11404,
               ""name"":""James Mainwaring"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327154,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-10T19:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":21,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":130160,
               ""name"":""Daniel Cook"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11461,
               ""name"":""Tim Wood"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11423,
               ""name"":""Andy Madley"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11443,
               ""name"":""Chris Kavanagh"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11556,
               ""name"":""David Coote"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11552,
               ""name"":""Peter Kirkup"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327166,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-10T19:45:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11595,
               ""name"":""Adrian Holmes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11487,
               ""name"":""Kevin Friend"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11570,
               ""name"":""Harry Lennard"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327080,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-12T12:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327081,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-12T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327075,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-12T17:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327073,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-13T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327074,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-13T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327076,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-13T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327077,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-13T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327079,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-13T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327082,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-13T16:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327078,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-14T20:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":29,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327210,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-16T19:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":16,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            {
               ""id"":11480,
               ""name"":""Eddie Smart"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11521,
               ""name"":""Mark Scholes"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11610,
               ""name"":""Andre Marriner"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11446,
               ""name"":""Robert Jones"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11488,
               ""name"":""Simon Bennett"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327102,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-16T20:15:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":27,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327167,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-17T19:45:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":20,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-19T00:20:16Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            {
               ""id"":11581,
               ""name"":""Gary Beswick"",
               ""role"":""ASSISTANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11586,
               ""name"":""Richard West"",
               ""role"":""ASSISTANT_REFEREE_N2"",
               ""nationality"":""England""
            },
            {
               ""id"":11551,
               ""name"":""Martin Atkinson"",
               ""role"":""FOURTH_OFFICIAL"",
               ""nationality"":""England""
            },
            {
               ""id"":11503,
               ""name"":""Graham Scott"",
               ""role"":""REFEREE"",
               ""nationality"":""England""
            },
            {
               ""id"":11479,
               ""name"":""Lee Mason"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N1"",
               ""nationality"":""England""
            },
            {
               ""id"":11615,
               ""name"":""Adam Nunn"",
               ""role"":""VIDEO_ASSISANT_REFEREE_N2"",
               ""nationality"":""England""
            }
         ]
      },
      {
         ""id"":327063,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-18T20:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327072,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-19T12:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327066,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-19T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327068,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-19T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327071,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-19T17:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327064,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-20T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-21T16:20:15Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327065,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-20T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327067,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-20T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-21T16:20:16Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327070,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-20T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-27T00:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327069,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-03-20T16:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":30,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-01-27T00:20:17Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327058,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-02T11:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:20Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327053,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-02T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327054,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-02T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327055,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-02T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327057,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-02T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:20Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327062,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-02T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:20Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327059,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-02T16:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327061,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-03T13:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327060,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-03T15:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327056,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-04T19:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":31,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327049,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-08T19:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327046,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-09T11:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327043,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-09T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327045,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-09T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327047,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-09T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327051,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-09T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327052,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-09T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:19Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327044,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-09T16:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327050,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-10T13:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327048,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-10T15:30:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":32,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2022-02-24T08:20:18Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327033,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327034,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327035,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327036,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327037,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327038,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327039,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:55Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327040,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:55Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327041,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:55Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327042,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-16T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":33,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:55Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327023,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327024,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327025,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327026,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327027,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327028,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327029,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327030,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327031,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327032,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-23T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":34,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:54Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327013,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327014,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327015,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327016,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327017,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327018,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327019,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327020,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327021,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327022,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-04-30T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":35,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:53Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327003,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327004,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327005,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327006,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327007,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327008,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327009,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327010,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327011,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327012,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-07T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":36,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:52Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326993,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""awayTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326994,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""awayTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326995,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""awayTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326996,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""awayTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326997,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""awayTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326998,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""awayTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326999,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""awayTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327000,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""awayTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327001,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:50Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""awayTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":327002,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-15T14:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":37,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:51Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""awayTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326983,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:48Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":68,
            ""name"":""Norwich City FC""
         },
         ""awayTeam"":{
            ""id"":73,
            ""name"":""Tottenham Hotspur FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326984,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:48Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":65,
            ""name"":""Manchester City FC""
         },
         ""awayTeam"":{
            ""id"":58,
            ""name"":""Aston Villa FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326985,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":64,
            ""name"":""Liverpool FC""
         },
         ""awayTeam"":{
            ""id"":76,
            ""name"":""Wolverhampton Wanderers FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326986,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":338,
            ""name"":""Leicester City FC""
         },
         ""awayTeam"":{
            ""id"":340,
            ""name"":""Southampton FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326987,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":354,
            ""name"":""Crystal Palace FC""
         },
         ""awayTeam"":{
            ""id"":66,
            ""name"":""Manchester United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326988,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":61,
            ""name"":""Chelsea FC""
         },
         ""awayTeam"":{
            ""id"":346,
            ""name"":""Watford FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326989,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":328,
            ""name"":""Burnley FC""
         },
         ""awayTeam"":{
            ""id"":67,
            ""name"":""Newcastle United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326990,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":397,
            ""name"":""Brighton & Hove Albion FC""
         },
         ""awayTeam"":{
            ""id"":563,
            ""name"":""West Ham United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326991,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":402,
            ""name"":""Brentford FC""
         },
         ""awayTeam"":{
            ""id"":341,
            ""name"":""Leeds United FC""
         },
         ""referees"":[
            
         ]
      },
      {
         ""id"":326992,
         ""season"":{
            ""id"":733,
            ""startDate"":""2021-08-13"",
            ""endDate"":""2022-05-22"",
            ""currentMatchday"":27
         },
         ""utcDate"":""2022-05-22T15:00:00Z"",
         ""status"":""SCHEDULED"",
         ""matchday"":38,
         ""stage"":""REGULAR_SEASON"",
         ""group"":null,
         ""lastUpdated"":""2021-06-16T14:31:49Z"",
         ""odds"":{
            ""msg"":""Activate Odds-Package in User-Panel to retrieve odds.""
         },
         ""score"":{
            ""winner"":null,
            ""duration"":""REGULAR"",
            ""fullTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""halfTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""extraTime"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            },
            ""penalties"":{
               ""homeTeam"":null,
               ""awayTeam"":null
            }
         },
         ""homeTeam"":{
            ""id"":57,
            ""name"":""Arsenal FC""
         },
         ""awayTeam"":{
            ""id"":62,
            ""name"":""Everton FC""
         },
         ""referees"":[
            
         ]
      }
   ]
}";

            footballDataStandingsJson = @"{
   ""filters"":{
      
   },
   ""competition"":{
      ""id"":2021,
      ""area"":{
         ""id"":2072,
         ""name"":""England""
      },
      ""name"":""Premier League"",
      ""code"":""PL"",
      ""plan"":""TIER_ONE"",
      ""lastUpdated"":""2021-04-17T02:20:14Z""
   },
   ""season"":{
      ""id"":733,
      ""startDate"":""2021-08-13"",
      ""endDate"":""2022-05-22"",
      ""currentMatchday"":27,
      ""winner"":null
   },
   ""standings"":[
      {
         ""stage"":""REGULAR_SEASON"",
         ""type"":""TOTAL"",
         ""group"":null,
         ""table"":[
            {
               ""position"":1,
               ""team"":{
                  ""id"":65,
                  ""name"":""Manchester City FC"",
                  ""crest"":""https://crests.football-data.org/65.png""
               },
               ""playedGames"":27,
               ""form"":null,
               ""won"":21,
               ""draw"":3,
               ""lost"":3,
               ""points"":66,
               ""goalsFor"":64,
               ""goalsAgainst"":17,
               ""goalDifference"":47
            },
            {
               ""position"":2,
               ""team"":{
                  ""id"":64,
                  ""name"":""Liverpool FC"",
                  ""crest"":""https://crests.football-data.org/64.png""
               },
               ""playedGames"":26,
               ""form"":null,
               ""won"":18,
               ""draw"":6,
               ""lost"":2,
               ""points"":60,
               ""goalsFor"":70,
               ""goalsAgainst"":20,
               ""goalDifference"":50
            },
            {
               ""position"":3,
               ""team"":{
                  ""id"":61,
                  ""name"":""Chelsea FC"",
                  ""crest"":""https://crests.football-data.org/61.png""
               },
               ""playedGames"":25,
               ""form"":null,
               ""won"":14,
               ""draw"":8,
               ""lost"":3,
               ""points"":50,
               ""goalsFor"":49,
               ""goalsAgainst"":18,
               ""goalDifference"":31
            },
            {
               ""position"":4,
               ""team"":{
                  ""id"":66,
                  ""name"":""Manchester United FC"",
                  ""crest"":""https://crests.football-data.org/66.png""
               },
               ""playedGames"":27,
               ""form"":null,
               ""won"":13,
               ""draw"":8,
               ""lost"":6,
               ""points"":47,
               ""goalsFor"":44,
               ""goalsAgainst"":34,
               ""goalDifference"":10
            },
            {
               ""position"":5,
               ""team"":{
                  ""id"":563,
                  ""name"":""West Ham United FC"",
                  ""crest"":""https://crests.football-data.org/563.png""
               },
               ""playedGames"":27,
               ""form"":null,
               ""won"":13,
               ""draw"":6,
               ""lost"":8,
               ""points"":45,
               ""goalsFor"":46,
               ""goalsAgainst"":34,
               ""goalDifference"":12
            },
            {
               ""position"":6,
               ""team"":{
                  ""id"":57,
                  ""name"":""Arsenal FC"",
                  ""crest"":""https://crests.football-data.org/57.png""
               },
               ""playedGames"":24,
               ""form"":null,
               ""won"":14,
               ""draw"":3,
               ""lost"":7,
               ""points"":45,
               ""goalsFor"":38,
               ""goalsAgainst"":27,
               ""goalDifference"":11
            },
            {
               ""position"":7,
               ""team"":{
                  ""id"":73,
                  ""name"":""Tottenham Hotspur FC"",
                  ""crest"":""https://crests.football-data.org/73.svg""
               },
               ""playedGames"":25,
               ""form"":null,
               ""won"":13,
               ""draw"":3,
               ""lost"":9,
               ""points"":42,
               ""goalsFor"":35,
               ""goalsAgainst"":32,
               ""goalDifference"":3
            },
            {
               ""position"":8,
               ""team"":{
                  ""id"":76,
                  ""name"":""Wolverhampton Wanderers FC"",
                  ""crest"":""https://crests.football-data.org/76.svg""
               },
               ""playedGames"":26,
               ""form"":null,
               ""won"":12,
               ""draw"":4,
               ""lost"":10,
               ""points"":40,
               ""goalsFor"":24,
               ""goalsAgainst"":21,
               ""goalDifference"":3
            },
            {
               ""position"":9,
               ""team"":{
                  ""id"":340,
                  ""name"":""Southampton FC"",
                  ""crest"":""https://crests.football-data.org/340.png""
               },
               ""playedGames"":26,
               ""form"":null,
               ""won"":8,
               ""draw"":11,
               ""lost"":7,
               ""points"":35,
               ""goalsFor"":34,
               ""goalsAgainst"":37,
               ""goalDifference"":-3
            },
            {
               ""position"":10,
               ""team"":{
                  ""id"":397,
                  ""name"":""Brighton & Hove Albion FC"",
                  ""crest"":""https://crests.football-data.org/397.svg""
               },
               ""playedGames"":26,
               ""form"":null,
               ""won"":7,
               ""draw"":12,
               ""lost"":7,
               ""points"":33,
               ""goalsFor"":25,
               ""goalsAgainst"":30,
               ""goalDifference"":-5
            },
            {
               ""position"":11,
               ""team"":{
                  ""id"":354,
                  ""name"":""Crystal Palace FC"",
                  ""crest"":""https://crests.football-data.org/354.png""
               },
               ""playedGames"":27,
               ""form"":null,
               ""won"":6,
               ""draw"":12,
               ""lost"":9,
               ""points"":30,
               ""goalsFor"":37,
               ""goalsAgainst"":38,
               ""goalDifference"":-1
            },
            {
               ""position"":12,
               ""team"":{
                  ""id"":58,
                  ""name"":""Aston Villa FC"",
                  ""crest"":""https://crests.football-data.org/58.svg""
               },
               ""playedGames"":25,
               ""form"":null,
               ""won"":9,
               ""draw"":3,
               ""lost"":13,
               ""points"":30,
               ""goalsFor"":33,
               ""goalsAgainst"":37,
               ""goalDifference"":-4
            },
            {
               ""position"":13,
               ""team"":{
                  ""id"":338,
                  ""name"":""Leicester City FC"",
                  ""crest"":""https://crests.football-data.org/338.png""
               },
               ""playedGames"":23,
               ""form"":null,
               ""won"":7,
               ""draw"":6,
               ""lost"":10,
               ""points"":27,
               ""goalsFor"":37,
               ""goalsAgainst"":43,
               ""goalDifference"":-6
            },
            {
               ""position"":14,
               ""team"":{
                  ""id"":67,
                  ""name"":""Newcastle United FC"",
                  ""crest"":""https://crests.football-data.org/67.png""
               },
               ""playedGames"":25,
               ""form"":null,
               ""won"":5,
               ""draw"":10,
               ""lost"":10,
               ""points"":25,
               ""goalsFor"":28,
               ""goalsAgainst"":45,
               ""goalDifference"":-17
            },
            {
               ""position"":15,
               ""team"":{
                  ""id"":402,
                  ""name"":""Brentford FC"",
                  ""crest"":""https://crests.football-data.org/402.png""
               },
               ""playedGames"":27,
               ""form"":null,
               ""won"":6,
               ""draw"":6,
               ""lost"":15,
               ""points"":24,
               ""goalsFor"":27,
               ""goalsAgainst"":44,
               ""goalDifference"":-17
            },
            {
               ""position"":16,
               ""team"":{
                  ""id"":341,
                  ""name"":""Leeds United FC"",
                  ""crest"":""https://crests.football-data.org/341.png""
               },
               ""playedGames"":26,
               ""form"":null,
               ""won"":5,
               ""draw"":8,
               ""lost"":13,
               ""points"":23,
               ""goalsFor"":29,
               ""goalsAgainst"":60,
               ""goalDifference"":-31
            },
            {
               ""position"":17,
               ""team"":{
                  ""id"":62,
                  ""name"":""Everton FC"",
                  ""crest"":""https://crests.football-data.org/62.png""
               },
               ""playedGames"":24,
               ""form"":null,
               ""won"":6,
               ""draw"":4,
               ""lost"":14,
               ""points"":22,
               ""goalsFor"":28,
               ""goalsAgainst"":41,
               ""goalDifference"":-13
            },
            {
               ""position"":18,
               ""team"":{
                  ""id"":328,
                  ""name"":""Burnley FC"",
                  ""crest"":""https://crests.football-data.org/328.png""
               },
               ""playedGames"":24,
               ""form"":null,
               ""won"":3,
               ""draw"":12,
               ""lost"":9,
               ""points"":21,
               ""goalsFor"":22,
               ""goalsAgainst"":30,
               ""goalDifference"":-8
            },
            {
               ""position"":19,
               ""team"":{
                  ""id"":346,
                  ""name"":""Watford FC"",
                  ""crest"":""https://crests.football-data.org/346.svg""
               },
               ""playedGames"":26,
               ""form"":null,
               ""won"":5,
               ""draw"":4,
               ""lost"":17,
               ""points"":19,
               ""goalsFor"":25,
               ""goalsAgainst"":47,
               ""goalDifference"":-22
            },
            {
               ""position"":20,
               ""team"":{
                  ""id"":68,
                  ""name"":""Norwich City FC"",
                  ""crest"":""https://upload.wikimedia.org/wikipedia/en/8/8c/Norwich_City.svg""
               },
               ""playedGames"":26,
               ""form"":null,
               ""won"":4,
               ""draw"":5,
               ""lost"":17,
               ""points"":17,
               ""goalsFor"":15,
               ""goalsAgainst"":55,
               ""goalDifference"":-40
            }
         ]
      }
   ]
}";

            mockHttpAPIClient = new Mock<IHttpAPIClient>();

            mockHttpAPIClient.Setup(x => x.GetAsync($"competitions/{footballEngineInput.Competition}/teams/")).ReturnsAsync(footballDataTeamsJson);

            mockHttpAPIClient.Setup(x => x.GetAsync($"competitions/{footballEngineInput.Competition}/matches/")).ReturnsAsync(footballDataMatchesJson);

            mockHttpAPIClient.Setup(x => x.GetAsync($"competitions/{footballEngineInput.Competition}/standings/")).ReturnsAsync(footballDataStandingsJson);
        }

        [TestClass]
        public class GetGroupsOrLeagueTableAsync : FootballDataServiceTest
        {
            [TestMethod]
            public async Task WhenGroupsOrLeagueTableFootballDataNeverCachedAndCompetitionDoesNotUseGroups_ThenStandingsReturnedFromAPIAndCacheRefreshed()
            {
                SetupTests();

                footballDataState = new FootballDataState();

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var groupsOrLeagueTable = await footballDataService.GetGroupsOrLeagueTableAsync();

                Assert.IsNotNull(groupsOrLeagueTable);

                var firstGroupOrLeagueTable = groupsOrLeagueTable.FirstOrDefault();

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/standings/"), Times.Once());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Never());

                Assert.IsNotNull(firstGroupOrLeagueTable);
                Assert.AreEqual("Premier League Table", firstGroupOrLeagueTable.Name);
                Assert.AreEqual(20, firstGroupOrLeagueTable.GroupOrLeagueTableStandings.Count);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenGroupsOrLeagueTableFootballDataCachedMoreThanOrEqualToNumberOfHoursToRefreshTime_ThenStandingsReturnedFromAPIButMatchesReturnedFromCacheAfterCacheRefresh()
            {
                SetupTests();

                var footballDataStandings = JsonSerializer.Deserialize<FootballDataModel>(footballDataStandingsJson);

                var footballDataMatches = JsonSerializer.Deserialize<FootballDataModel>(footballDataMatchesJson);

                footballDataState = new FootballDataState()
                {
                    FootballDataStandings = footballDataStandings,
                    FootballDataMatches = footballDataMatches,
                    CompetitionStartDate = footballDataStandings.season.startDate,
                    LastRefreshTime = DateTime.UtcNow.AddHours(-footballEngineInput.HoursUntilRefreshCache),
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var groupsOrLeagueTable = await footballDataService.GetGroupsOrLeagueTableAsync();

                Assert.IsNotNull(groupsOrLeagueTable);

                var firstGroupOrLeagueTable = groupsOrLeagueTable.FirstOrDefault();

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/standings/"), Times.Once());

                Assert.IsNotNull(firstGroupOrLeagueTable);
                Assert.AreEqual("Premier League Table", firstGroupOrLeagueTable.Name);
                Assert.AreEqual(20, firstGroupOrLeagueTable.GroupOrLeagueTableStandings.Count);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenGroupsOrLeagueTableFootballDataStandingsAndMatchesCachedLessThanNumberOfHoursToRefreshTime_ThenStandingsAndMatchesReturnedFromCache()
            {
                SetupTests();

                var footballDataStandings = JsonSerializer.Deserialize<FootballDataModel>(footballDataStandingsJson);

                var footballDataMatches = JsonSerializer.Deserialize<FootballDataModel>(footballDataMatchesJson);

                footballDataState = new FootballDataState()
                {
                    FootballDataStandings = footballDataStandings,
                    FootballDataMatches = footballDataMatches,
                    CompetitionStartDate = footballDataStandings.season.startDate,
                    LastRefreshTime = DateTime.UtcNow.AddHours(-footballEngineInput.HoursUntilRefreshCache + 0.0001),
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var groupsOrLeagueTable = await footballDataService.GetGroupsOrLeagueTableAsync();

                Assert.IsNotNull(groupsOrLeagueTable);

                var firstGroupOrLeagueTable = groupsOrLeagueTable.FirstOrDefault();

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/standings/"), Times.Never);
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Never());

                Assert.IsNotNull(firstGroupOrLeagueTable);
                Assert.AreEqual("Premier League Table", firstGroupOrLeagueTable.Name);
                Assert.AreEqual(20, firstGroupOrLeagueTable.GroupOrLeagueTableStandings.Count);
                Assert.IsFalse(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenOnlyGroupsOrLeagueTableFootballDataMatchesCached_ThenMatchesReturnedFromCacheButStandingsReturnedFromAPI()
            {
                SetupTests();

                var footballDataMatches = JsonSerializer.Deserialize<FootballDataModel>(footballDataMatchesJson);

                footballDataState = new FootballDataState()
                {
                    FootballDataStandings = null,
                    FootballDataMatches = footballDataMatches,
                    LastRefreshTime = DateTime.UtcNow,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var groupsOrLeagueTable = await footballDataService.GetGroupsOrLeagueTableAsync();

                Assert.IsNotNull(groupsOrLeagueTable);

                var firstGroupOrLeagueTable = groupsOrLeagueTable.FirstOrDefault();

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/standings/"), Times.Once());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Never());

                Assert.IsNotNull(firstGroupOrLeagueTable);
                Assert.AreEqual("Premier League Table", firstGroupOrLeagueTable.Name);
                Assert.AreEqual(20, firstGroupOrLeagueTable.GroupOrLeagueTableStandings.Count);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }
        }

        [TestClass]
        public class GetTeamsAsync : FootballDataServiceTest
        {
            [TestMethod]
            public async Task WhenTeamsNeverCached_ThenTeamsReturnedFromAPIAndCacheRefreshed()
            {
                SetupTests();

                footballDataState = new FootballDataState();

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var teams = await footballDataService.GetTeamsAsync();

                Assert.IsNotNull(teams);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Once());

                Assert.AreEqual(20, teams.Count);

                var firstTeam = teams.FirstOrDefault();

                Assert.IsNotNull(firstTeam);
                Assert.AreEqual("Arsenal", firstTeam.Name);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenTeamsFootballDataStateNotNullButTeamsCacheIsNull_ThenTeamsReturnedFromAPIAndCacheRefreshed()
            {
                SetupTests();

                footballDataState = new FootballDataState()
                {
                    Teams = null,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var teams = await footballDataService.GetTeamsAsync();

                Assert.IsNotNull(teams);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Once());

                Assert.AreEqual(20, teams.Count);

                var firstTeam = teams.FirstOrDefault();

                Assert.IsNotNull(firstTeam);
                Assert.AreEqual("Arsenal", firstTeam.Name);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenTeamsCached_ThenTeamsReturnedFromCache()
            {
                SetupTests();

                var footballDataTeams = JsonSerializer.Deserialize<Teams>(footballDataTeamsJson);

                var testTeams = footballDataTeams
                            .teams
                            .ToList()
                            .Select(x => new FootballShared.Models.Team()
                            {
                                TeamID = x.id,
                                Name = x.shortName,
                                TeamCrestUrl = x.crest,
                            })
                            .OrderBy(x => x.Name)
                            .ToList();

                footballDataState = new FootballDataState()
                {
                    Teams = testTeams,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var teams = await footballDataService.GetTeamsAsync();

                Assert.IsNotNull(teams);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Never());

                Assert.AreEqual(20, teams.Count);

                var firstTeam = teams.FirstOrDefault();

                Assert.IsNotNull(firstTeam);
                Assert.AreEqual("Arsenal", firstTeam.Name);
                Assert.IsFalse(footballDataState.IsCacheRefreshed);
            }
        }

        [TestClass]
        public class GetTeamAsync : FootballDataServiceTest
        {
            int newcastleTeamId = 67;

            [TestMethod]
            public async Task WhenTeamsNeverCached_ThenTeamsAndTeamSquadAndMatchesReturnedFromAPIAndCacheRefreshed()
            {
                teamId = newcastleTeamId;

                SetupTests();

                footballDataState = new FootballDataState();

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var team = await footballDataService.GetTeamAsync(newcastleTeamId);

                Assert.IsNotNull(team);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Once());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Once());

                Assert.AreEqual("Newcastle", team.Name);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenTeamFootballDataStateNotNullButTeamsCacheIsNull_ThenTeamsAndTeamSquadAndMatchesReturnedFromAPIAndCacheRefreshed()
            {
                teamId = newcastleTeamId;

                SetupTests();

                footballDataState = new FootballDataState()
                {
                    Teams = null,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var team = await footballDataService.GetTeamAsync(newcastleTeamId);

                Assert.IsNotNull(team);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Once());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Once());

                Assert.AreEqual("Newcastle", team.Name);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenTeamsCachedButTeamSquadAndMatchesNotCached_ThenTeamsReturnedFromCacheButTeamSquadAndMatchesReturnedFromAPI()
            {
                teamId = newcastleTeamId;

                SetupTests();

                var footballDataTeams = JsonSerializer.Deserialize<Teams>(footballDataTeamsJson);

                var testTeams = footballDataTeams
                            .teams
                            .Where(x => x.id == newcastleTeamId)
                            .ToList()
                            .Select(x => new FootballShared.Models.Team()
                            {
                                TeamID = x.id,
                                Name = x.shortName,
                                TeamCrestUrl = x.crest,
                                Squad = x.squad
                                            .ToList()
                                            .Select(x => new Player()
                                            {
                                                Name = x.name 
                                            })
                                            .ToList(),
                            })
                            .OrderBy(x => x.Name)
                            .ToList();

                footballDataState = new FootballDataState()
                {
                    Teams = testTeams,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var team = await footballDataService.GetTeamAsync(newcastleTeamId);

                Assert.IsNotNull(team);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Never());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Once());

                Assert.AreEqual("Newcastle", team.Name);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenTeamsAndTeamSquadCachedButMatchesAreNotCached_ThenTeamsAndTeamSquadReturnedFromCacheButMatchesReturnedFromAPI()
            {
                teamId = newcastleTeamId;

                SetupTests();

                var footballDataTeams = JsonSerializer.Deserialize<Teams>(footballDataTeamsJson);

                var testTeams = footballDataTeams
                            .teams
                            .Where(x => x.id == newcastleTeamId)
                            .ToList()
                            .Select(x => new FootballShared.Models.Team()
                            {
                                TeamID = x.id,
                                Name = x.shortName,
                                TeamCrestUrl = x.crest,
                                Squad = new List<Player>()
                                {
                                    new Player()
                                    {
                                        PlayerID = 8446,
                                        Name = "Allan Saint-Maximin",
                                        Position = "Attacker",
                                        TeamID = 67,
                                    },
                                    new Player()
                                    {
                                        PlayerID = 3312,
                                        Name = "Kieran Trippier",
                                        Position = "Defender",
                                        TeamID = 67,
                                    },
                                }
                            })
                            .OrderBy(x => x.Name)
                            .ToList();

                footballDataState = new FootballDataState()
                {
                    Teams = testTeams,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var team = await footballDataService.GetTeamAsync(newcastleTeamId);

                Assert.IsNotNull(team);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Never());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"teams/{newcastleTeamId}"), Times.Never());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Once());

                Assert.AreEqual("Newcastle", team.Name);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenTeamsAndTeamSquadAndMatchesCached_ThenTeamsAndTeamSquadAndMatchesReturnedFromCache()
            {
                teamId = newcastleTeamId;

                SetupTests();

                var footballDataTeams = JsonSerializer.Deserialize<Teams>(footballDataTeamsJson);

                var testTeams = footballDataTeams
                            .teams
                            .Where(x => x.id == newcastleTeamId)
                            .ToList()
                            .Select(x => new FootballShared.Models.Team()
                            {
                                TeamID = x.id,
                                Name = x.shortName,
                                TeamCrestUrl = x.crest,
                                Squad = new List<Player>()
                                {
                                    new Player()
                                    {
                                        PlayerID = 8446,
                                        Name = "Allan Saint-Maximin",
                                        Position = "Attacker",
                                        TeamID = 67,
                                    },
                                    new Player()
                                    {
                                        PlayerID = 3312,
                                        Name = "Kieran Trippier",
                                        Position = "Defender",
                                        TeamID = 67,
                                    },
                                }
                            })
                            .OrderBy(x => x.Name)
                            .ToList();

                var footballDataMatches = JsonSerializer.Deserialize<FootballDataModel>(footballDataMatchesJson);

                footballDataState = new FootballDataState()
                {
                    Teams = testTeams,
                    FootballDataMatches = footballDataMatches,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var team = await footballDataService.GetTeamAsync(newcastleTeamId);

                Assert.IsNotNull(team);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/teams/"), Times.Never());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"teams/{newcastleTeamId}"), Times.Never());
                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Never());

                Assert.AreEqual("Newcastle", team.Name);
                Assert.IsFalse(footballDataState.IsCacheRefreshed);
            }
        }

        [TestClass]
        public class GetFixturesAndResultsByDaysAsync : FootballDataServiceTest
        {
            [TestMethod]
            public async Task WhenFixturesNeverCached_ThenFootbalDataFixturesReturnedFromAPIAndCacheRefreshed()
            {
                SetupTests();

                footballDataState = new FootballDataState();

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var fixturesAndResultsByDays = await footballDataService.GetFixturesAndResultsByDaysAsync();

                Assert.IsNotNull(fixturesAndResultsByDays);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Once());

                Assert.AreEqual(9, fixturesAndResultsByDays.Count);

                var firstFixture = fixturesAndResultsByDays.FirstOrDefault();

                Assert.IsNotNull(firstFixture);
                Assert.AreEqual(new DateTime(2021, 08, 13), firstFixture.FixtureDate);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            public async Task WhenFixturesFootballDataStateNotNullButFootballDataMatchesCacheIsNull_ThenFootbalDataFixturesReturnedFromAPIAndCacheRefreshed()
            {
                SetupTests();

                footballDataState = new FootballDataState()
                {
                    FootballDataMatches = null,
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var fixturesAndResultsByDays = await footballDataService.GetFixturesAndResultsByDaysAsync();

                Assert.IsNotNull(fixturesAndResultsByDays);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Once());

                Assert.AreEqual(106, fixturesAndResultsByDays.Count);

                var firstFixture = fixturesAndResultsByDays.FirstOrDefault();

                Assert.IsNotNull(firstFixture);
                Assert.AreEqual(new DateTime(2021, 08, 13), firstFixture.FixtureDate);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenFixturesCachedMoreThanOrEqualToNumberOfHoursToRefreshTime_ThenFixturesReturnedFromAPIAndCacheRefreshed()
            {
                SetupTests();

                var footballDataMatches = JsonSerializer.Deserialize<FootballDataModel>(footballDataMatchesJson);

                footballDataState = new FootballDataState()
                {
                    FootballDataMatches = footballDataMatches,
                    CompetitionStartDate = footballDataMatches.matches.ToList().First().season.startDate,
                    LastRefreshTime = DateTime.UtcNow.AddHours(-footballEngineInput.HoursUntilRefreshCache),
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var fixturesAndResultsByDays = await footballDataService.GetFixturesAndResultsByDaysAsync();

                Assert.IsNotNull(fixturesAndResultsByDays);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Once());

                Assert.AreEqual(9, fixturesAndResultsByDays.Count);

                var firstFixture = fixturesAndResultsByDays.FirstOrDefault();

                Assert.IsNotNull(firstFixture);
                Assert.AreEqual(new DateTime(2021, 08, 13), firstFixture.FixtureDate);
                Assert.IsTrue(footballDataState.IsCacheRefreshed);
            }

            [TestMethod]
            public async Task WhenFixturesCachedLessThanNumberOfHoursToRefreshTime_ThenFixturesReturnedFromCache()
            {
                SetupTests();

                var footballDataMatches = JsonSerializer.Deserialize<FootballDataModel>(footballDataMatchesJson);

                footballDataState = new FootballDataState()
                {
                    FootballDataMatches = footballDataMatches,
                    CompetitionStartDate = footballDataMatches.matches.ToList().First().season.startDate,
                    LastRefreshTime = DateTime.UtcNow.AddHours(-footballEngineInput.HoursUntilRefreshCache + 0.0001),
                };

                var footballDataService = new FootballDataService(mockHttpAPIClient.Object, footballDataState, footballEngineInput);

                var fixturesAndResultsByDays = await footballDataService.GetFixturesAndResultsByDaysAsync();

                Assert.IsNotNull(fixturesAndResultsByDays);

                mockHttpAPIClient.Verify(mock => mock.GetAsync($"competitions/{footballEngineInput.Competition}/matches/"), Times.Never());

                Assert.AreEqual(9, fixturesAndResultsByDays.Count);

                var firstFixture = fixturesAndResultsByDays.FirstOrDefault();

                Assert.IsNotNull(firstFixture);
                Assert.AreEqual(new DateTime(2021, 08, 13), firstFixture.FixtureDate);
                Assert.IsFalse(footballDataState.IsCacheRefreshed);
            }
        }
    }
}

