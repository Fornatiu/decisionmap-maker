using System;
using CSharpFunctionalExtensions;
using Domain.Aggregates.DecisionMapAggregate.Entities;


namespace Domain.Aggregates.DecisionMapAggregate.Repositories
{
    public interface IDecisionMapRepository
    {
        //public Task<List<DecisionMap>> GetDecisionMapsAsync();
        public Task<DecisionMap?> GetDecisionMapByIdAsync(Guid decisionMapId);
        public Task<List<DecisionMap?>> GetDecisionMapsByUserIdAsync(Guid userAcountId);
        public Task AddDecisionMapAsync(DecisionMap decisionMap);
        public void UpdateDecisionMapAsync(DecisionMap newDecisionMap);
        public void DeleteDecisionMapAsync(DecisionMap decisionMap);
    }
}

