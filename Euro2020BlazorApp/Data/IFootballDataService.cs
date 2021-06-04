using Euro2020BlazorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro2020BlazorApp.Data
{
    interface IFootballDataService
    {
        public Task<List<Group>> GetGroups();
        public Task<Group> GetGroup(string groupName);
        public Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays();
        public Task<FixturesAndResultsByGroup> GetFixturesAndResultsByGroup(Group group);
    }
}
