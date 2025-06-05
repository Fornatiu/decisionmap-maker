using CSharpFunctionalExtensions;
using Domain.Aggregates.QrMasterAggregate.Entities;

namespace Domain.Aggregates.QrMasterAggregate.Repositories
{
    public interface IQrMasterRepository
    {
        public Task<List<QrMaster>> GetAllQrAsync();
        public Task<QrMaster?> GetQrByIdAsync(Guid idQr);
        public Task AddQrAsync(QrMaster qr);
        public Task UpdateQrAsync(QrMaster newQr);
        public Task DeleteQrAsync(Guid idQr);
    }
}
