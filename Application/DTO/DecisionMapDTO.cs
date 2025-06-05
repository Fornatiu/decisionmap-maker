using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public sealed record DecisionMapDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    int QrCount);
}
