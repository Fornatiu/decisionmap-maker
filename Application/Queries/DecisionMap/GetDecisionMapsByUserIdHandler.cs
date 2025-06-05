using Application.DTO;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using MediatR;

namespace Application.Queries.DecisionMap
{
    public sealed class GetDecisionMapsByUserIdHandler
    : IRequestHandler<GetDecisionMapsByUserIdQuery, List<DecisionMapDto>>
    {
        private readonly IDecisionMapRepository _repo;
        public GetDecisionMapsByUserIdHandler(IDecisionMapRepository repo) => _repo = repo;

        public async Task<List<DecisionMapDto>> Handle(GetDecisionMapsByUserIdQuery q, CancellationToken ct)
            => (await _repo.GetDecisionMapsByUserIdAsync(q.UserId))
               .Select(ToDto)
               .ToList();

        private static DecisionMapDto ToDto(Domain.Aggregates.DecisionMapAggregate.Entities.DecisionMap p)
            => new(p.Id, p.Name, p.TimeStamp, p.SelectedQrs.Count);
    }

}
