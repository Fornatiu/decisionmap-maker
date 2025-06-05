using CSharpFunctionalExtensions;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using MediatR;

namespace Application.Commands.DecisionMap
{
    //public class DeleteDecisionMapHandler : IRequestHandler<DeleteDecisionMapCommand, Result>
    //{
    //    private readonly IDecisionMapRepository _decisionMapRepository;

    //    public DeleteDecisionMapHandler(IDecisionMapRepository decisionMapRepository)
    //    {
    //        _decisionMapRepository = decisionMapRepository;
    //    }

    //    public async Task<Result> Handle(DeleteDecisionMapCommand request, CancellationToken cancellationToken)
    //    {
    //        var decisionMap = await _decisionMapRepository.GetDecisionMapByIdAsync(request.DecisionMapId);
    //        if (decisionMap is null)
    //            return Result.Failure("Cart not found.");

    //        return await _decisionMapRepository.DeleteDecisionMapAsync(request.DecisionMapId);
    //    }
    //}

    public sealed class DeleteDecisionMapHandler : IRequestHandler<DeleteDecisionMapCommand, Result>
    {
        private readonly IDecisionMapRepository _repo;

        public DeleteDecisionMapHandler(IDecisionMapRepository repo) => _repo = repo;

        public async Task<Result> Handle(DeleteDecisionMapCommand d, CancellationToken ct)
        {
            var project = await _repo.GetDecisionMapByIdAsync(d.ProjectId);
            if (project is null)
                return Result.Failure("Project not found");

            _repo.DeleteDecisionMapAsync(project);
            return Result.Success();  // UoW will commit deletion
        }
    }

}
