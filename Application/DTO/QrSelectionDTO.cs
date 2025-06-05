using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.Value_Objects;

namespace Application.DTO
{
    public sealed record QrSelectionDto(Guid QrMasterId, DefaultImpact ImpactLevel, SusteinabilityDimension Dimension);
}
