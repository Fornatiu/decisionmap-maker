using CSharpFunctionalExtensions;
using Domain.Aggregates.DecisionMapAggregate.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.DecisionMap
{
    //public class CreateDecisionMapCommand : IRequest<Result<Guid>>
    //{
    //    [Required] public Guid UserAccountId { get; set; }
    //    [Required] public string ProjectName { get; set; }
    //    //[Required] public List<ProjectQr> SelectedQrs { get; set; }
    //    //[Required] public List<DependencyEdge> DMatrix { get; set; }
    //    //[Required] public DateTime TimeStamp { get; set; } = DateTime.Now;
    //}
    public sealed record CreateDecisionMapCommand(Guid OwnerUserId, string ProjectName)
    : IRequest<Result<Guid>>;

}


