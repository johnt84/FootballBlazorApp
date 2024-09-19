using FootballShared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FootballBlazorApp.Components;

public partial class FixturesAndResultsByDayComponent
{
    [Parameter]
    public Enums.ComponentSource ComponentSource { get; set; }

    [Parameter]
    public FootballShared.Models.FixturesAndResultsByDay FixturesAndResultsByDayModel { get; set; }

    private DateTime FixtureDate;

    private bool FixturesAndResultsExist => FixturesAndResultsByDayModel != null
                            && (FixturesAndResultsByDayModel.FixturesAndResults?.Count ?? 0) > 0;


    private string TableStyle => ComponentSource == Enums.ComponentSource.FixturesAndResults && !IsSmall
                                        ? "width: 670px; margin-bottom: 20px;"
                                        : "width: 340px; margin-bottom: 20px;";

    private List<FootballShared.Models.FixtureAndResult> FixturesAndResults;

    private bool IsSmall = false;

    protected override void OnInitialized()
    {
        FixtureDate = FixturesAndResultsExist ? FixturesAndResultsByDayModel.FixtureDate : DateTime.MinValue;
        FixturesAndResults = FixturesAndResultsByDayModel
                                .FixturesAndResults.OrderBy(x => x.FixtureDate)
                                .ThenBy(x => x.HomeTeam.Name)
                                .ToList();
    }
}
