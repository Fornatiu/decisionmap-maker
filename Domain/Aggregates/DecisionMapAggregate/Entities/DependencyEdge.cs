using Domain.Aggregates.Value_Objects;

namespace Domain.Aggregates.DecisionMapAggregate.Entities
{
    public class DependencyEdge : BaseEntity
    {

        public Guid FromQrId { get; set; }
        public Guid ToQrId { get; set; }
        public EdgeEffect Effect { get; set; } = EdgeEffect.Undefined;
        public DependencyEdge() { }
        public DependencyEdge(Guid fromQrId, Guid toQrId, EdgeEffect effect)
        {
            FromQrId = fromQrId;
            ToQrId = toQrId;
            Effect = effect;
        }
        internal void ChangeEffect(EdgeEffect newEffect) => Effect = newEffect;
    }
}
