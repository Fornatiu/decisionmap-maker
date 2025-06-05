using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Entities;
using Domain.Aggregates.Value_Objects;
using MediatR;

namespace Application.Commands.QrMaster
{
    public class AddQrCommand : IRequest<Result>
    {
        [Required] public required string Name { get; set; }
        [Required] public required SusteinabilityDimension Dimension { get; set; }
    }
}
