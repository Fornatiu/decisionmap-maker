using Domain.Aggregates.QrMasterAggregate.Repositories;
using MediatR;

namespace Application.Queries.QrMaster
{
    public class GetQrMasterByIdHandler : IRequestHandler<GetQrMasterByIdQuery, Domain.Aggregates.QrMasterAggregate.Entities.QrMaster>
    {
        private readonly IQrMasterRepository _qrMasterRepository;

        public GetQrMasterByIdHandler(IQrMasterRepository qrMasterRepository)
        {
            this._qrMasterRepository = qrMasterRepository;
        }

        public async Task<Domain.Aggregates.QrMasterAggregate.Entities.QrMaster> Handle(GetQrMasterByIdQuery request, CancellationToken cancellationToken)
        {
            //if(_userAccountRepository.GetUserAccountByIdAsync(request.userId) == null)
            //{
            //    throw new Domain.Aggregates.UserAggregate.Exceptions.UserAccountException("You are not logged in!\n");
            //}
            var qr = await _qrMasterRepository.GetQrByIdAsync(request.QrMasterId);
            return qr;
        }
    }
}