﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public sealed record GraphDto(
    IReadOnlyCollection<object> Nodes,
    IReadOnlyCollection<object> Edges);
}
