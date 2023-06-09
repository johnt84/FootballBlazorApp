using FootballDataEngine.Queries;
using FootballEngine.Services.Interfaces;
using FootballShared.Models;
using MediatR;

namespace FootballDataEngine.Handlers
{
    public class GetFixturesAndResultsByDayHandler : IRequestHandler<GetFixturesAndResultsByDayQuery, List<FixturesAndResultsByDay>>
    {

        private readonly IFootballDataService _footballDataService = null;

        public GetFixturesAndResultsByDayHandler(IFootballDataService footballDataService)
        {
            _footballDataService = footballDataService;
        }

        public async Task<List<FixturesAndResultsByDay>> Handle(GetFixturesAndResultsByDayQuery request, CancellationToken token)
        {
            return await _footballDataService.GetFixturesAndResultsByDaysAsync();
        }
    }
}
