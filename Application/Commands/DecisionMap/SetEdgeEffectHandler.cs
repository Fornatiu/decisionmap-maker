using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.DecisionMapAggregate.Repositories;

namespace Application.Commands.DecisionMap
{
    public sealed class SetEdgeEffectHandler : IRequestHandler<SetEdgeEffectCommand, Result>
    {
        private readonly IDecisionMapRepository _repo;

        public SetEdgeEffectHandler(IDecisionMapRepository repo) => _repo = repo;

        public async Task<Result> Handle(SetEdgeEffectCommand cmd, CancellationToken ct)
        {
            var project = await _repo.GetDecisionMapByIdAsync(cmd.ProjectId);
            if (project is null)
                return Result.Failure("Project not found");

            var r = project.SetEdge(cmd.FromQrId, cmd.ToQrId, cmd.Effect);
            return r; // Success / Failure propagated; UoW saves if success
        }
    }
}
