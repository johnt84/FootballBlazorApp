using FootballDataEngine.Queries;
using FootballShared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class FixturesAndResults
    {
        private List<FixturesAndResultsByDay> fixturesAndResultsByDays = null;

        private bool isInvalidFixturesAndResults = false;

        private string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                fixturesAndResultsByDays = await Mediator.Send(new GetFixturesAndResultsByDayQuery());

                fixturesAndResultsByDays = await UpdateFixtureTimeWithLocalOffset(fixturesAndResultsByDays);

                isInvalidFixturesAndResults = false;
            }
            catch (Exception ex) when (ex.Message.Contains(Constants.TOO_MANY_REQUESTS_ERROR_CODE.ToString()))
            {
                isInvalidFixturesAndResults = true;
                ErrorMessage = Constants.TOO_MANY_REQUESTS_ERROR_MESSAGE;
            }
            catch (Exception)
            {
                isInvalidFixturesAndResults = true;
                ErrorMessage = $"Could not retrieve any fixtures & results";
            }
        }

        private async Task<List<FixturesAndResultsByDay>> UpdateFixtureTimeWithLocalOffset(List<FixturesAndResultsByDay> fixturesAndResultsByDays)
        {
            int offsetInMinutesForUser = await TimeZoneOffsetService.GetLocalOffsetInMinutesForUser();

            foreach (var fixtureByDay in fixturesAndResultsByDays)
            {
                foreach (var fixtureAndResult in fixtureByDay.FixturesAndResults)
                {
                    fixtureAndResult.FixtureDate = fixtureAndResult.FixtureDate.AddMinutes(-offsetInMinutesForUser);
                }

            }

            return fixturesAndResultsByDays;
        }
    }
}
