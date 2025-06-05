using Application.DTO;
using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DecisionMap
{
    public sealed record UpsertProjectQrsCommand(
    Guid ProjectId,
    IReadOnlyCollection<QrSelectionDto> Selections)
    : IRequest<Result>;
}
