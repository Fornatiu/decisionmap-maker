using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Entities;
using Domain.Aggregates.Value_Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.QrMaster
{
    public class UpdateQrCommand : IRequest<Result>
    {
        [Required] public required Guid Id { get; set; }
        [Required] public required string NewName { get; set; }
        [Required] public required SusteinabilityDimension NewDimension { get; set; }
    }
}
