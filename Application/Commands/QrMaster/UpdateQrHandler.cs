using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using MediatR;

namespace Application.Commands.QrMaster
{
    internal class UpdateQrHandler : IRequestHandler<UpdateQrCommand, Result>
    {
        private readonly IQrMasterRepository _qrMasterRepository;

        public UpdateQrHandler(IQrMasterRepository qrMasterRepository)
        {
            _qrMasterRepository = qrMasterRepository;
        }

        public async Task<Result> Handle(UpdateQrCommand request, CancellationToken cancellationToken)
        {
            var qrMaster = await _qrMasterRepository.GetQrByIdAsync(request.Id);
            if (qrMaster == null)
                return Result.Failure("Product not found.");
            qrMaster.Name = request.NewName;
            qrMaster.Dimension = request.NewDimension;
            await _qrMasterRepository.UpdateQrAsync(qrMaster);
            return Result.Success();
        }
    }
}
