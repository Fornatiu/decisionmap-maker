using Application.DTO;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using MediatR;

namespace Application.Queries.DecisionMap
{
    public sealed class GetMatrixHandler : IRequestHandler<GetMatrixQuery, MatrixDto>
    {
        private readonly IDecisionMapRepository _repo;
        public GetMatrixHandler(IDecisionMapRepository repo) => _repo = repo;

        public async Task<MatrixDto> Handle(GetMatrixQuery q, CancellationToken ct)
        {
            var p = await _repo.GetDecisionMapByIdAsync(q.ProjectId) ?? throw new InvalidOperationException("Project not found");
            var nodes = p.SelectedQrs.Select(qr => new QrSelectionDto(qr.QrMasterId, qr.ImpactLevel, qr.Dimension)).ToList();
            var edges = p.DMatrix.Select(e => new MatrixEdgeDto(e.FromQrId, e.ToQrId, e.Effect)).ToList();
            return new MatrixDto(nodes, edges);
        }
    }
}
