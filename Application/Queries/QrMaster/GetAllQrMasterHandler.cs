using Domain.Aggregates.QrMasterAggregate.Repositories;
using Application.DTO;
using MediatR;

namespace Application.Queries.QrMaster
{
    public class GetAllQrMasterHandler : IRequestHandler<GetAllQrMasterQuery, List<QrMasterDTO>>
    {
        private readonly IQrMasterRepository _qrMasterRepository;
        public GetAllQrMasterHandler(IQrMasterRepository qrMasterRepository)
        {
            _qrMasterRepository = qrMasterRepository;
        }
        public async Task<List<QrMasterDTO>> Handle(GetAllQrMasterQuery request, CancellationToken cancellationToken)
        {
            var qr = await _qrMasterRepository.GetAllQrAsync();
            return qr.Select(p => new QrMasterDTO(p.Id, p.Name, p.Dimension)).ToList();
        }
    }
}
