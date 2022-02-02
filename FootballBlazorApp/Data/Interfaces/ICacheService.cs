using FootballBlazorApp.Models;
using System.Threading.Tasks;

namespace FootballBlazorApp.Data
{
    public interface ICacheService
    {
        Task SaveToCacheAsync(FootballDataState footballDataState);
        Task<FootballDataState> LoadFromCacheAsync();
    }
}
