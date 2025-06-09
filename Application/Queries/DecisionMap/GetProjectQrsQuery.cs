using Application.DTO;
using Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DecisionMap
{
    public sealed record GetProjectQrsQuery(Guid ProjectId)
    : IRequest<List<QrSelectionDto>>, IQueryMarker;
}
