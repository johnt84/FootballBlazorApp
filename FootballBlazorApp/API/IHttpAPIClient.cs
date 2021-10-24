using System.Threading.Tasks;

namespace FootballBlazorApp.API
{
    interface IHttpAPIClient
    {
        public Task<string> Get(string url);
    }
}
