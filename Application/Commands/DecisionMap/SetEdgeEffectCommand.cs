using CSharpFunctionalExtensions;
using Domain.Aggregates.Value_Objects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DecisionMap
{
    public sealed record SetEdgeEffectCommand(
    Guid ProjectId,
    Guid FromQrId,
    Guid ToQrId,
    EdgeEffect Effect) : IRequest<Result>;
}
