using Application.DTO;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using MediatR;

namespace Application.Queries.DecisionMap
{
    public class GetDecisionMapByIdQuery : IRequest<DecisionMapDto>
    {
        public Guid DecisionMapId { get; set; }

        public GetDecisionMapByIdQuery(Guid decisionMapId)
        {
            DecisionMapId = decisionMapId;
        }

    }
}
