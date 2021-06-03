using System.Threading.Tasks;

namespace Euro2020BlazorApp.API
{
    interface IHttpAPIClient
    {
        public Task<string> Get(string url);
    }
}
