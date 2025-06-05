using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.QrMaster
{
    public class GetQrMasterByIdQuery : IRequest<Domain.Aggregates.QrMasterAggregate.Entities.QrMaster>
    {
        public Guid QrMasterId { get; set; }
        public GetQrMasterByIdQuery(Guid qrMasterId)
        {
            this.QrMasterId = qrMasterId;
        }
    }
}
