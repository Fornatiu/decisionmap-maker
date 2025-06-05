using Domain.Aggregates.QrMasterAggregate.Entities;
using Domain.Aggregates.QrMasterAggregate.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class QrMasterRepository : IQrMasterRepository
{
    private readonly ApplicationDbContext _db;

    public QrMasterRepository(ApplicationDbContext db) => _db = db;

    
    public Task AddQrAsync(QrMaster qr)
    {
        _db.QrMaster.Add(qr);       
        return Task.CompletedTask;
    }


    public async Task UpdateQrAsync(QrMaster updated)
    {
        var current = await _db.QrMaster.FindAsync(new object[] { updated.Id });
        if (current is null)
            throw new KeyNotFoundException("QR not found");

        current.Name = updated.Name;
        current.Dimension = updated.Dimension;

    }


    public async Task DeleteQrAsync(Guid id)
    {
        var qr = await _db.QrMaster.FindAsync(new object[] { id });
        if (qr is not null)
            _db.QrMaster.Remove(qr);
    }

    
    public Task<List<QrMaster>> GetAllQrAsync() =>
        _db.QrMaster.AsNoTracking().ToListAsync();

    public Task<QrMaster?> GetQrByIdAsync(Guid id) =>
        _db.QrMaster.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}
