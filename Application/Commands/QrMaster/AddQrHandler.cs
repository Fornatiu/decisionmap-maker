using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using MediatR;

namespace Application.Commands.QrMaster
{
    internal class AddQrHandler : IRequestHandler<AddQrCommand, Result>
    {
        private readonly IQrMasterRepository _qrMasterRepository;

        public AddQrHandler(IQrMasterRepository qrMasterRepository)
        {
            _qrMasterRepository = qrMasterRepository;
        }

        public async Task<Result> Handle(AddQrCommand request, CancellationToken cancellationToken)
        {
            var qrMaster = new Domain.Aggregates.QrMasterAggregate.Entities.QrMaster(request.Name , request.Dimension);            
            await _qrMasterRepository.AddQrAsync(qrMaster);
            return Result.Success();
        }

    }
}
