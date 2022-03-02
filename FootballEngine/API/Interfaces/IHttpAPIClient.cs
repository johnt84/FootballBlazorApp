namespace FootballEngine.API.Interfaces
{
    public interface IHttpAPIClient
    {
        public Task<string> GetAsync(string url);
    }
}
