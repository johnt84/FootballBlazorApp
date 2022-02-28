namespace FootballEngine.API
{
    public interface IHttpAPIClient
    {
        public Task<string> Get(string url);
    }
}
