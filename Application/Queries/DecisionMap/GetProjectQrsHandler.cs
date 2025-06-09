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
    public sealed class GetProjectQrsHandler
    : IRequestHandler<GetProjectQrsQuery,List<QrSelectionDto>>
    {
        private readonly IDecisionMapRepository _repo;
        public GetProjectQrsHandler(IDecisionMapRepository repo) => _repo = repo;

        public async Task<List<QrSelectionDto>> Handle(GetProjectQrsQuery q, CancellationToken ct)
        {
            var p = await _repo.GetDecisionMapByIdAsync(q.ProjectId);
            return p.SelectedQrs
                     .Select(qr => new QrSelectionDto(qr.QrMasterId, qr.ImpactLevel, qr.Dimension))
                     .ToList();
        }
    }
}
