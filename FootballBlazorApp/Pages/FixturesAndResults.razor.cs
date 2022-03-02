﻿using FootballShared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballBlazorApp.Pages
{
    public partial class FixturesAndResults
    {
        private List<FootballShared.Models.FixturesAndResultsByDay> fixturesAndResultsByDays = null;

        private bool isInvalidFixturesAndResults = false;

        private string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                fixturesAndResultsByDays = await FootballDataService.GetFixturesAndResultsByDaysAsync();
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
    }
}
