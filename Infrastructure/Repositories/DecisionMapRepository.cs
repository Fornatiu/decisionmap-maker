using CSharpFunctionalExtensions;
using Domain.Aggregates.DecisionMapAggregate.Entities;
using Domain.Aggregates.DecisionMapAggregate.Repositories;
using Infrastructure.Data;                   // Your DbContext
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class DecisionMapRepository : IDecisionMapRepository
    {
        private readonly ApplicationDbContext _context;

        public DecisionMapRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddDecisionMapAsync(DecisionMap project)
        {
            _context.DecisionMap.Add(project);           
            return Task.CompletedTask;
        }

        public void DeleteDecisionMapAsync(DecisionMap project)
        {
            _context.DecisionMap.Remove(project);       
        }

        public void UpdateDecisionMapAsync(DecisionMap project)
        {
            _context.DecisionMap.Update(project);
        }


        public async Task<DecisionMap?> GetDecisionMapByIdAsync(Guid id)
       => await _context.DecisionMap
                    .Include(p => p.SelectedQrs)
                    .Include(p => p.DMatrix)
                    .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<DecisionMap?>> GetDecisionMapsByUserIdAsync(Guid userId)
            => await _context.DecisionMap
                         .Where(p => p.UserId == userId)
                         .ToListAsync();

    }
}
