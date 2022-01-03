using System;
using System.Threading.Tasks;

namespace FootballBlazorApp.API
{
    public interface IHttpAPIClient
    {
        public Task<string> Get(string url);
    }
}
