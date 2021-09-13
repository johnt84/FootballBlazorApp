using Euro2020BlazorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro2020BlazorApp.Data
{
    public interface IFootballDataService
    {
        public Task<List<Group>> GetGroups();
        public Task<List<Models.Team>> GetTeams();
        public Task<Models.Team> GetTeam(int teamID);
        public Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays();
        public Task<List<Group>> GetFixturesAndResultsByGroups(List<Group> groups);
    }
}
