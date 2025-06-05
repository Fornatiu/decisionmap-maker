using Domain.Aggregates.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public sealed record MatrixEdgeDto(Guid FromId, Guid ToId, EdgeEffect Effect);

    public sealed record MatrixDto(
    IReadOnlyCollection<QrSelectionDto> Nodes,
    IReadOnlyCollection<MatrixEdgeDto> Edges);
}
