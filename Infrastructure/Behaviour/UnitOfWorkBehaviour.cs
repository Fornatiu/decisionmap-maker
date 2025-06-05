using Microsoft.Extensions.Logging;
using Infrastructure.Data;
using MediatR;
using Application.Services.Interfaces;

namespace Infrastructure.Behaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ApplicationDbContext _db;          
        private readonly ILogger<UnitOfWorkBehavior<TRequest, TResponse>> _log;

        public UnitOfWorkBehavior(ApplicationDbContext db,
                                  ILogger<UnitOfWorkBehavior<TRequest, TResponse>> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
            // Skip transactions for queries
            if (request is IQueryMarker)
                return await next();

            await using var tx = await _db.Database.BeginTransactionAsync(ct);

            try
            {
                var response = await next();         // handler
                await _db.SaveChangesAsync(ct);       
                await tx.CommitAsync(ct);
                return response;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "UnitOfWork rollback for {Request}", typeof(TRequest).Name);
                await tx.RollbackAsync(ct);
                throw;
            }
        }
    }
}
