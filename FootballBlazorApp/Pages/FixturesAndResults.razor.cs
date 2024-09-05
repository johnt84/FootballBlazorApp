using FootballShared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages;

public partial class FixturesAndResults
{
    private List<FootballShared.Models.FixturesAndResultsByDay> FixturesAndResultsByDays = null;

    private bool IsInvalidFixturesAndResults = false;

    private string ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            FixturesAndResultsByDays = await FootballDataService.GetFixturesAndResultsByDaysAsync();

            FixturesAndResultsByDays = await UpdateFixtureTimeWithLocalOffsetAsync(FixturesAndResultsByDays);

            IsInvalidFixturesAndResults = false;
        }
        catch (Exception ex) when (ex.Message.Contains(Constants.TOO_MANY_REQUESTS_ERROR_CODE.ToString()))
        {
            IsInvalidFixturesAndResults = true;
            ErrorMessage = Constants.TOO_MANY_REQUESTS_ERROR_MESSAGE;
        }
        catch (Exception)
        {
            IsInvalidFixturesAndResults = true;
            ErrorMessage = $"Could not retrieve any fixtures & results";
        }
    }

    private async Task<List<FixturesAndResultsByDay>> UpdateFixtureTimeWithLocalOffsetAsync(List<FixturesAndResultsByDay> fixturesAndResultsByDays)
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
