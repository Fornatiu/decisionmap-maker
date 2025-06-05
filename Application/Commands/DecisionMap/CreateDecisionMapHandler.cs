using CSharpFunctionalExtensions;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using MediatR;

namespace Application.Commands.DecisionMap
{
    //public class CreateDecisionMapHandler : IRequestHandler<CreateDecisionMapCommand, Result<Guid>>
    //{
    //    private readonly IDecisionMapRepository _decisionMapRepository;

    //    public CreateDecisionMapHandler(IDecisionMapRepository decisionMapRepository)
    //    {
    //        _decisionMapRepository = decisionMapRepository;
    //    }

    //    public async Task<Result<Guid>> Handle(CreateDecisionMapCommand request, CancellationToken cancellationToken)
    //    {
    //        var newDecisionMap = new Domain.Aggregates.DecisionMapAggregate.Entities.DecisionMap(request.UserAccountId,request.ProjectName);
    //        await _decisionMapRepository.AddDecisionMapAsync(newDecisionMap);
    //        return Result.Success(newDecisionMap.Id);
    //    }
    //}

    public sealed class CreateDecisionMapHandler : IRequestHandler<CreateDecisionMapCommand, Result<Guid>>
    {
        private readonly IDecisionMapRepository _decisionMapRepository;

        public CreateDecisionMapHandler(IDecisionMapRepository decisionMapRepository)
        {
            _decisionMapRepository = decisionMapRepository;
        }

        public async Task<Result<Guid>> Handle(CreateDecisionMapCommand request, CancellationToken ct)
        {
            var project = new Domain.Aggregates.DecisionMapAggregate.Entities.DecisionMap(request.OwnerUserId, request.ProjectName);
            await _decisionMapRepository.AddDecisionMapAsync(project);               // staged ‑ unit‑of‑work will commit
            return Result.Success(project.Id);
        }
    }

}
