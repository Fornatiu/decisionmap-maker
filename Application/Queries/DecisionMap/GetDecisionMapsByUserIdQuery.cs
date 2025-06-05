using Application.DTO;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Queries.DecisionMap
{
    //public class GetDecisionMapsByUserIdQuery : IRequest<List<Domain.Aggregates.DecisionMapAggregate.Entities.DecisionMap>>
    //{
    //    public Guid UserAccountId { get; set; }

    //    public GetDecisionMapsByUserIdQuery(Guid userId)
    //    {
    //        UserAccountId = userId;
    //    }
    //}
    public sealed record GetDecisionMapsByUserIdQuery(Guid UserId)
    : IRequest<List<DecisionMapDto>>, IQueryMarker;

}
