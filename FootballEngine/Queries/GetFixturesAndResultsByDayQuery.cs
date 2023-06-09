using FootballShared.Models;
using MediatR;

namespace FootballDataEngine.Queries
{
    public record GetFixturesAndResultsByDayQuery() : IRequest<List<FixturesAndResultsByDay>>;
}
