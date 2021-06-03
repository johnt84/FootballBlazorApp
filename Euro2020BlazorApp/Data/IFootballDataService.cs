using Euro2020BlazorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro2020BlazorApp.Data
{
    interface IFootballDataService
    {
        public Task<List<Euro2020BlazorApp.Models.Group>> GetGroups();
        public Task<List<Fixture>> GetFixtures();
    }
}
