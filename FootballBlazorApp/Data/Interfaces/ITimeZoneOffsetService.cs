using System.Threading.Tasks;

namespace FootballBlazorApp.Data
{
    public interface ITimeZoneOffsetService
    {
        public ValueTask<int> GetLocalOffsetInMinutes();
    }
}
