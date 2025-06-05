using CSharpFunctionalExtensions;
using MediatR;

namespace Application.Commands.DecisionMap
{
    //public class DeleteDecisionMapCommand : IRequest<Result>
    //{
    //    public Guid DecisionMapId { get; set; }

    //    public DeleteDecisionMapCommand(Guid cartId)
    //    {
    //        DecisionMapId = cartId;
    //    }
    //}

    public sealed record DeleteDecisionMapCommand(Guid ProjectId) : IRequest<Result>;

}
