using Microsoft.AspNetCore.Components;

namespace FootballBlazorApp.Components;

public partial class FixturesAndResultsByGroupOrLeagueTable
{
    private bool FixturesAndResultsExist => FixturesAndResultsByGroupOrLeagueTableModel != null
                                            && (FixturesAndResultsByGroupOrLeagueTableModel.FixturesAndResults?.Count ?? 0) > 0;

    [Parameter]
    public FootballShared.Models.FixturesAndResultsByGroupOrLeagueTable FixturesAndResultsByGroupOrLeagueTableModel { get; set; }

    private bool IsSmall = false;

    private string TableStyle => !IsSmall
                                     ? "width: 450px; margin-bottom: 20px;"
                                     : "width: 340px; margin-bottom: 20px;";
}
