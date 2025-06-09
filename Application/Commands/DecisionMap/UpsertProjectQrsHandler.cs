using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using MediatR;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using System.Text;

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

            var incoming = cmd.Selections
                          .Select(s => s.QrMasterId)
                          .ToHashSet();

            foreach (var sel in cmd.Selections)
            {
                var master = await _catalog.GetQrByIdAsync(sel.QrMasterId);
                if (master is null)
                    return Result.Failure($"QR {sel.QrMasterId} not found in catalogue.");

                var addResult = project.AddQr(
                                    master.Id,
                                    sel.ImpactLevel,
                                    master.Dimension);

                if (addResult.IsFailure && addResult.Error != "QR already selected.")
                    return Result.Failure(addResult.Error);
            }

            var toRemove = project.SelectedQrs
                              .Where(q => !incoming.Contains(q.QrMasterId))
                              .Select(q => q.QrMasterId)
                              .ToList();

            foreach (var qrId in toRemove)
            {
                var rm = project.RemoveQr(qrId);
                _repo.HardDeleteProjectQr(rm.Value);
                if (rm.IsFailure)
                    return Result.Failure(rm.Error);
            }

            return Result.Success();
        }
    }
}
