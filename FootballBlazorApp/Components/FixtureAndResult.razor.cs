using FootballShared.Models;
using Microsoft.AspNetCore.Components;

namespace FootballBlazorApp.Components;

public partial class FixtureAndResult
{
    [Parameter]
    public Enums.ComponentSource ComponentSource { get; set; }

    [Parameter]
    public FootballShared.Models.FixtureAndResult FixtureAndResultModel { get; set; }

    private const string TBD = "TBD";

    private string GetStageNameForDisplay(FootballShared.Models.Enums.Stage stage) => stage.ToString().Replace("_", " ");

    private string GetStageName(FootballShared.Models.FixtureAndResult fixtureAndResult) =>
                                        fixtureAndResult.Stage == Enums.Stage.Group
                                        ? fixtureAndResult.GroupOrLeagueTable.Name
                                        : GetStageNameForDisplay(fixtureAndResult.Stage);

    private bool TeamExists(Team team) => team != null && !string.IsNullOrEmpty(team.Name);

    private string GetTeamName(Team team) => TeamExists(team) ? team.Name : TBD;

    private string GetScore(FootballShared.Models.FixtureAndResult fixtureAndResult) => $"{fixtureAndResult.HomeTeamGoals} - {fixtureAndResult.AwayTeamGoals}";

    private string GetScoreOrKickOffTime(FootballShared.Models.FixtureAndResult fixtureAndResult) =>
                                    fixtureAndResult.GameStatus == Enums.GameStatus.Scheduled
                                    ? fixtureAndResult.FixtureDate.ToString("HH:mm")
                                    : GetScore(fixtureAndResult);
}
