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
    public sealed class GetGraphJsonHandler : IRequestHandler<GetGraphJsonQuery, GraphDto>
    {
        private readonly IDecisionMapRepository _repo;
        public GetGraphJsonHandler(IDecisionMapRepository repo) => _repo = repo;

        public async Task<GraphDto> Handle(GetGraphJsonQuery q, CancellationToken ct)
        {
            var p = await _repo.GetDecisionMapByIdAsync(q.ProjectId) ?? throw new InvalidOperationException("Project not found");

            // node & edge shape kept generic (object) so front‑end can choose its own schema.
            var nodes = p.SelectedQrs.Select(qr => new {
                id = qr.Id,
                masterId = qr.QrMasterId,
                dim = qr.Dimension.ToString(),
                impact = qr.ImpactLevel.ToString()
            }).ToList<object>();

            var edges = p.DMatrix.Select(e => new {
                id = e.Id,
                source = e.FromQrId,
                target = e.ToQrId,
                effect = e.Effect.ToString()
            }).ToList<object>();

            return new GraphDto(nodes, edges);
        }
    }
}
