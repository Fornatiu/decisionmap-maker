using Domain.Aggregates.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public sealed record QrMasterDTO(Guid QrMasterID,string Name, SusteinabilityDimension Dimension);
}
