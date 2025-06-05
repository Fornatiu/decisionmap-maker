using Application.DTO;
using MediatR;

namespace Application.Queries.QrMaster
{
    public class GetAllQrMasterQuery : IRequest<List<QrMasterDTO>>
    {
    }
}
