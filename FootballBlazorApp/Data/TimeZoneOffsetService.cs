using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace FootballBlazorApp.Data
{
    public sealed class TimeZoneOffsetService : ITimeZoneOffsetService
    {
        private readonly IJSRuntime _jsRuntime;

        public TimeZoneOffsetService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async ValueTask<int> GetLocalOffsetInMinutes()
        {
            return await _jsRuntime.InvokeAsync<int>("blazorGetTimezoneOffset");
        }
    }
}
