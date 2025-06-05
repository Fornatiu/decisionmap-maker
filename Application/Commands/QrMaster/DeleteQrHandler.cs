using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using MediatR;

namespace Application.Commands.QrMaster
{
    internal class DeleteQrHandler : IRequestHandler<DeleteQrCommand, Result>
    {
        private readonly IQrMasterRepository _qrMasterRepository;
        public DeleteQrHandler(IQrMasterRepository qrMasterRepository)
        {
            _qrMasterRepository = qrMasterRepository;
        }
        public async Task<Result> Handle(DeleteQrCommand request, CancellationToken cancellationToken)
        {
            var qrMaster = await _qrMasterRepository.GetQrByIdAsync(request.Id);

            if (qrMaster == null)
                return Result.Failure("Product not found.");

            await _qrMasterRepository.DeleteQrAsync(request.Id);

            return Result.Success();
        }
    }
}
