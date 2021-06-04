using System.Threading.Tasks;

namespace Euro2020BlazorApp.Data
{
    public interface ITimeZoneOffsetService
    {
        public ValueTask<int> GetLocalOffsetInMinutes();
    }
}
