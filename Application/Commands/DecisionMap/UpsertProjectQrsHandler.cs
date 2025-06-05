using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using MediatR;
using Domain.Aggregates.DecisionMapAggregate.Repositories;

namespace Application.Commands.DecisionMap
{
    public sealed class UpsertProjectQrsHandler : IRequestHandler<UpsertProjectQrsCommand, Result>
    {
        private readonly IDecisionMapRepository _repo;
        private readonly IQrMasterRepository _catalog;

        public UpsertProjectQrsHandler(IDecisionMapRepository repo, IQrMasterRepository catalog)
        {
            _repo = repo;
            _catalog = catalog;
        }

        public async Task<Result> Handle(UpsertProjectQrsCommand cmd, CancellationToken ct)
        {
            var project = await _repo.GetDecisionMapByIdAsync(cmd.ProjectId);
            if (project is null)
                return Result.Failure("Project not found");

            foreach (var sel in cmd.Selections)
            {
                var master = await _catalog.GetQrByIdAsync(sel.QrMasterId);
                if (master is null)
                    return Result.Failure($"QR {sel.QrMasterId} not found in catalogue");

                var r = project.AddQr(master.Id, sel.ImpactLevel, master.Dimension);
                if (r.IsFailure)
                    return Result.Failure(r.Error);
            }
            return Result.Success();  // UoW behavior will persist
        }
    }
}
