using Domain.Aggregates.Value_Objects;

namespace Domain.Aggregates.DecisionMapAggregate.Entities
{
    public class ProjectQr : BaseEntity
    {
        public Guid QrMasterId { get; set; }
        public DefaultImpact ImpactLevel { get; set; }
        public SusteinabilityDimension Dimension { get; set; }
        public ProjectQr() { }

        public ProjectQr(Guid qrmasterid, DefaultImpact impact, SusteinabilityDimension dimension)
        {
            QrMasterId = qrmasterid;
            ImpactLevel = impact;
            Dimension = dimension;
        }
    }
}