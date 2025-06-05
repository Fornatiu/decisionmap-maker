using Application.DTO;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DecisionMap
{
    public sealed class GetDecisionMapByIdHandler
    : IRequestHandler<GetDecisionMapByIdQuery, DecisionMapDto?>
    {
        private readonly IDecisionMapRepository _repo;
        public GetDecisionMapByIdHandler(IDecisionMapRepository repo) => _repo = repo;

        public async Task<DecisionMapDto?> Handle(GetDecisionMapByIdQuery q, CancellationToken ct)
        {
            var p = await _repo.GetDecisionMapByIdAsync(q.DecisionMapId);
            return p is null ? null : new DecisionMapDto(p.Id, p.Name, p.TimeStamp, p.SelectedQrs.Count);
        }
    }
}
